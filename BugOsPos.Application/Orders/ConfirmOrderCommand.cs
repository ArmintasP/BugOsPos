using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Application.Orders;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
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

        if (request.CustomerId is int requestCustomerId)
        {
            var customerId = CustomerId.New(requestCustomerId);

            if (!customerId.Equals(order.CustomerId))
                return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
        }

        else if (request.EmployeeId is int requestEmployeeId)
        {
            var employeeId = EmployeeId.New(requestEmployeeId);
            var employee = await _employeeRepository.GetEmployeeById(employeeId);

            if (order.CashierId is EmployeeId orderCashierId)
            {
                var cashier = await _employeeRepository.GetEmployeeById(orderCashierId);
                if (cashier?.GroupId is null || !cashier.GroupId.Equals(employee?.GroupId))
                     return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
            }

            if (order.CourierId is EmployeeId orderCourierId)
            {
                var courier = await _employeeRepository.GetEmployeeById(orderCourierId);
                if (courier?.GroupId is null || !courier.GroupId.Equals(employee?.GroupId))
                    return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
            }
        }

        if (order.Status != OrderStatus.NotPlaced)
            return Domain.Common.ErrorsCollection.Errors.Order.AlreadyConfirmed;

        order.Confirm();
        await _orderRepository.Update(order);

        return new ConfirmOrderResult();
    }
}
