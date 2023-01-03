using BugOsPos.Application.Common.Interfaces.Clock;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Orders;

public sealed record RateOrderProductCommand(
    int Id,
    int OrderItemId,
    int CustomerId,
    decimal RatingNumber) : IRequest<ErrorOr<RateOrderProductResult>>;

public sealed record RateOrderProductResult(int ProductId, decimal Rating, int NumberOfRatings);

public sealed class RateOrderProductValidator : AbstractValidator<RateOrderProductCommand>
{
    public RateOrderProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.RatingNumber)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(10);
    }
}

public sealed class RateOrderProductCommandHandler : IRequestHandler<RateOrderProductCommand, ErrorOr<RateOrderProductResult>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public RateOrderProductCommandHandler(
        IEmployeeRepository employeeRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _employeeRepository = employeeRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<RateOrderProductResult>> Handle(RateOrderProductCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.New(request.Id);

        if (await _orderRepository.GetOrderById(orderId) is not Order order)
            return Domain.Common.ErrorsCollection.Errors.Order.NotFound;

        var isValid = await OrderChecks.IsValidCaller(order, (request.CustomerId, null), _employeeRepository);
        if (isValid.IsError)
            return isValid.Errors;

        if (order.Payment is not Payment)
            return Domain.Common.ErrorsCollection.Errors.Order.PaymentIsMissing;

        var orderItem = order.OrderItems
            .SingleOrDefault(x => x.Id.Value == request.OrderItemId);

        if (orderItem is not OrderItem)
            return Domain.Common.ErrorsCollection.Errors.Order.OrderItemNotFound;

        if (orderItem.IsRated)
            return Domain.Common.ErrorsCollection.Errors.Order.OrderItemAlreadyRated;

        var product = await _productRepository.GetProductById(orderItem.ProductId);
        if (product is not Product)
            return Domain.Common.ErrorsCollection.Errors.Order.ProductNoLongerExists;

        product.Rating.AddNewRating(request.RatingNumber);
        orderItem.IsRated = true;

        await _productRepository.Update(product);
        await _orderRepository.UpdateOrderItem(orderItem);

        return new RateOrderProductResult(product.Id.Value, product.Rating.Value, product.Rating.NumberOfRatings);
    }
}
