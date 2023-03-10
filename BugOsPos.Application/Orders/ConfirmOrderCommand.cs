using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Orders;

public sealed record ConfirmOrderCommand(
    int Id,
    int? CustomerId,
    int? EmployeeId) : IRequest<ErrorOr<ConfirmOrderResult>>;

public sealed record ConfirmOrderResult();

public sealed class ConfirmOrderValidator : AbstractValidator<ConfirmOrderCommand>
{
    public ConfirmOrderValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ErrorOr<ConfirmOrderResult>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOrderRepository _orderRepository;

    public ConfirmOrderCommandHandler(IEmployeeRepository employeeRepository, IOrderRepository orderRepository)
    {
        _employeeRepository = employeeRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<ConfirmOrderResult>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.New(request.Id);

        if (await _orderRepository.GetOrderById(orderId) is not Order order)
            return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
        
        var isValid = await OrderChecks.IsValidCaller(order, (request.CustomerId, request.EmployeeId), _employeeRepository);
        if (isValid.IsError)
            return isValid.Errors;

        if (order.Status != OrderStatus.NotPlaced)
            return Domain.Common.ErrorsCollection.Errors.Order.AlreadyConfirmed;

        order.Confirm();
        await _orderRepository.Update(order);

        return new ConfirmOrderResult();
    }
}
