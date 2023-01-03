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

public sealed record GetOrderInvoiceCommand(
    int Id,
    int EmployeeId) : IRequest<ErrorOr<GetOrderInvoiceResult>>;

public sealed record GetOrderInvoiceResult(Payment Payment, string Invoice);

public sealed class GetOrderInvoiceValidator : AbstractValidator<GetOrderInvoiceCommand>
{
    public GetOrderInvoiceValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetOrderInvoiceCommandHandler : IRequestHandler<GetOrderInvoiceCommand, ErrorOr<GetOrderInvoiceResult>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOrderRepository _orderRepository;

    public GetOrderInvoiceCommandHandler(IEmployeeRepository employeeRepository, IOrderRepository orderRepository, IClock clock)
    {
        _employeeRepository = employeeRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<GetOrderInvoiceResult>> Handle(GetOrderInvoiceCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.New(request.Id);

        if (await _orderRepository.GetOrderById(orderId) is not Order order)
            return Domain.Common.ErrorsCollection.Errors.Order.NotFound;

        var isValid = await OrderChecks.IsValidCaller(order, (null, request.EmployeeId), _employeeRepository);
        if (isValid.IsError)
            return isValid.Errors;

        if (order.Payment is not Payment payment)
            return Domain.Common.ErrorsCollection.Errors.Order.PaymentIsMissing;

        var invoice = $"{payment.Id.Value}-invoice";

        return new GetOrderInvoiceResult(payment, invoice);
    }
}
