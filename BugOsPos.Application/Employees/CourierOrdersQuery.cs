using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Employees;
public sealed record CourierOrdersQuery(int Id) : IRequest<ErrorOr<CourierOrdersResult>>;

public sealed record CourierOrdersResult(List<Order> orders);

public sealed class CourierOrdersValidator : AbstractValidator<CourierOrdersQuery>
{
    public CourierOrdersValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class CourierOrdersQueryHandler : IRequestHandler<CourierOrdersQuery, ErrorOr<CourierOrdersResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public CourierOrdersQueryHandler(IOrderRepository orderRepository, IEmployeeRepository employeeRepository)
    {
        _orderRepository = orderRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<CourierOrdersResult>> Handle(CourierOrdersQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeById(EmployeeId.New(request.Id));
        if (employee is null)
            return Domain.Common.ErrorsCollection.Errors.Employee.NotFound;
        if (!employee.Roles.Any(role => role is EmployeeRole.Courier))
            return Domain.Common.ErrorsCollection.Errors.Employee.NotCourier;

        var orders = await _orderRepository.GetOrdersByCourierId(employee.Id);
        
        return new CourierOrdersResult(orders.ToList());
    }
}