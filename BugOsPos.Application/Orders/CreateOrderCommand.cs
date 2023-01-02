using BugOsPos.Application.Common.Behaviors;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Orders;

public sealed record CreateOrderCommand(
    int? CustomerId,
    int? CashierId,
    int LocationId,
    string? CustomerComment,
    bool IsDelivery,
    string PaymentType,
    List<int> OrderItems) : IRequest<ErrorOr<CreateOrderResult>>;

public sealed record CreateOrderResult(Order Order);

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.PaymentType).PaymentType();
    }
}

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<CreateOrderResult>>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CreateOrderResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.PaymentType, out PaymentType type))
            return Errors.Order.PaymentTypeIsNotValid;

        var order = Order.New(
            _orderRepository.NextIdentity().Value,
            null,
            request.CustomerId,
            request.CashierId,
            null,
            request.LocationId,
            request.IsDelivery);

        order.CustomerComment = request.CustomerComment;

        foreach(var orderItem in request.OrderItems)
        {
            if (await _orderRepository.GetOrderItemById(OrderItemId.New(orderItem)) is not OrderItem item)
                return Errors.Order.OrderItemNotFound;

            order.AddOrderItem(item);
        }

        await _orderRepository.Add(order);
        return new CreateOrderResult(order);
    }
}
