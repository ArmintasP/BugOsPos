using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        var isValid = await OrderChecks.IsValidCaller(order, (request.CustomerId, request.EmployeeId), _employeeRepository);
        if (isValid.IsError)
            return isValid.Errors;

        return new GetOrderByIdResult(order);
    }
}
