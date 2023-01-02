using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Orders;

public sealed record GetOrderByIdQuery(
    int Id,
    int? CustomerId,
    int? EmployeeId) : IRequest<ErrorOr<GetOrderByIdResult>>;

public sealed record GetOrderByIdResult(Order Order);

public sealed class GetOrderByIdValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ErrorOr<GetOrderByIdResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IEmployeeRepository employeeRepository)
    {
        _orderRepository = orderRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<GetOrderByIdResult>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.New(request.Id);

        if (await _orderRepository.GetOrderById(orderId) is not Order order)
            return Domain.Common.ErrorsCollection.Errors.Order.NotFound;


        if (request.CustomerId is int requestCustomerId)
        {
            var customerId = CustomerId.New(requestCustomerId);

            if (customerId.Equals(order.CustomerId))
                return new GetOrderByIdResult(order);
        }
        else if (request.EmployeeId is int requestEmployeeId)
        {
            var employeeId = EmployeeId.New(requestEmployeeId);
            var employee = await _employeeRepository.GetEmployeeById(employeeId);

            if (order.CashierId is EmployeeId orderCashierId)
            {
                var cashier = await _employeeRepository.GetEmployeeById(orderCashierId);
                if (cashier?.GroupId is not null && cashier.GroupId.Equals(employee?.GroupId))
                    return new GetOrderByIdResult(order);
            }

            if (order.CourierId is EmployeeId orderCourierId)
            {
                var courier = await _employeeRepository.GetEmployeeById(orderCourierId);
                if (courier?.GroupId is not null && courier.GroupId.Equals(employee?.GroupId))
                    return new GetOrderByIdResult(order);
            }
        }

        return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
    }
}
