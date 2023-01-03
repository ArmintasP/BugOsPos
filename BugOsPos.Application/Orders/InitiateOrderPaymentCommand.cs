using BugOsPos.Application.Common.Behaviors;
using BugOsPos.Application.Common.Interfaces.Clock;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Orders;

public sealed record InitiateOrderPaymentCommand(
    int Id,
    int? CustomerId,
    int? EmployeeId,
    string PaymentType) : IRequest<ErrorOr<InitiateOrderPaymentResult>>;

public sealed record InitiateOrderPaymentResult(Payment Payment);

public sealed class InitiateOrderPaymentValidator : AbstractValidator<InitiateOrderPaymentCommand>
{
    public InitiateOrderPaymentValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PaymentType).PaymentType();
    }
}

public sealed class InitiateOrderPaymentCommandHandler : IRequestHandler<InitiateOrderPaymentCommand, ErrorOr<InitiateOrderPaymentResult>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IClock _clock;

    public InitiateOrderPaymentCommandHandler(IEmployeeRepository employeeRepository, IOrderRepository orderRepository, IClock clock)
    {
        _employeeRepository = employeeRepository;
        _orderRepository = orderRepository;
        _clock = clock;
    }

    public async Task<ErrorOr<InitiateOrderPaymentResult>> Handle(InitiateOrderPaymentCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.PaymentType, out PaymentType type))
            return Domain.Common.ErrorsCollection.Errors.Order.PaymentTypeIsNotValid;

        var orderId = OrderId.New(request.Id);

        if (await _orderRepository.GetOrderById(orderId) is not Order order)
            return Domain.Common.ErrorsCollection.Errors.Order.NotFound;

        var isValid = await OrderChecks.IsValidCaller(order, (null, request.EmployeeId), _employeeRepository);
        if (isValid.IsError)
            return isValid.Errors;

        if (order.Payment is not null)
            return Domain.Common.ErrorsCollection.Errors.Order.PaymentAlreadyInitiated;

        order.Pay(type, _clock.UtcNow);
        await _orderRepository.Update(order);

        return new InitiateOrderPaymentResult(order.Payment!);
    }
}
