using BugOsPos.Application.Common.Behaviors;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Orders;

public sealed record CreateOrderCommand(
    int Id,
    int? CustomerId,
    int? CashierId,
    int LocationId,
    string? CustomerComment,
    bool IsDelivery,
    string PaymentType) : IRequest<ErrorOr<CreateOrderResult>>;

public sealed record CreateOrderResult(Order Order);

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
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

        if(await _orderRepository.GetOrderById(OrderId.New(request.Id)) is not Order order)
            return Errors.Order.NotFound;

        order = Order.New(
            order.Id.Value,
            order?.LoyaltyCardId?.Value,
            request.CustomerId ?? order?.CustomerId?.Value,
            request.CashierId ?? order?.CashierId?.Value,
            order?.CourierId?.Value,
            request.LocationId,
            request.IsDelivery);

        order.CustomerComment = request.CustomerComment;
        
        await _orderRepository.Update(order);
        return new CreateOrderResult(order);
    }
}
