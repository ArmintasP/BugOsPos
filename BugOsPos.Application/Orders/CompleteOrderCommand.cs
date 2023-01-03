using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Orders;

public sealed record CompleteOrderCommand(
    int Id,
    int EmployeeId) : IRequest<ErrorOr<CompleteOrderResult>>;

public sealed record CompleteOrderResult();

public sealed class CompleteOrderValidator : AbstractValidator<CompleteOrderCommand>
{
    public CompleteOrderValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, ErrorOr<CompleteOrderResult>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOrderRepository _orderRepository;

    public CompleteOrderCommandHandler(IEmployeeRepository employeeRepository, IOrderRepository orderRepository)
    {
        _employeeRepository = employeeRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CompleteOrderResult>> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.New(request.Id);

        if (await _orderRepository.GetOrderById(orderId) is not Order order)
            return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
        
        var isValid = await OrderChecks.IsValidCaller(order, (null, request.EmployeeId), _employeeRepository);
        if (isValid.IsError)
            return isValid.Errors;

        if (order.Status is OrderStatus.Closed)
            return Domain.Common.ErrorsCollection.Errors.Order.AlreadyCompleted;

        order.Complete();
        await _orderRepository.Update(order);

        return new CompleteOrderResult();
    }
}
