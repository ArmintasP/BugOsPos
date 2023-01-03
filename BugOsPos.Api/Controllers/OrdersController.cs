using BugOsPos.Application.Orders;
using BugOsPos.Contracts.Orders;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugOsPos.Api.Controllers;

public class OrdersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public OrdersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Gets the Order specified by id. Requires a Customer session having that order, or any Employee belonging to the Group that took the Order.
    /// </summary>
    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        GetOrderByIdQuery query;
        
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
        {
            if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var customerId))
                return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

            query = new GetOrderByIdQuery(id, customerId, null);
        }
        else
        {
            if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
                return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.NotFound });

            query = new GetOrderByIdQuery(id, null, employeeId);
        }

        var result = await _mediator.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<GetOrderByIdResponse>(result)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Creates an Order. Requires a Customer session having that order, or any Employee belonging to the Group that is creating the Order.
    /// </summary>
    [HttpPut("orders/{id}")]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
        {
            if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var customerId))
                return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

            var command = new UpdateOrderCommand(id, customerId, null, request.LocationId, request.CustomerComment, request.IsDelivery, request.PaymentType);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<UpdateOrderResponse>(result)),
                errors => Problem(errors));
        }
        else
        {
            if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
                return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.NotFound });

            var command = new UpdateOrderCommand(id, null, employeeId, request.LocationId, request.CustomerComment, request.IsDelivery, request.PaymentType);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<UpdateOrderResponse>(result)),
                errors => Problem(errors));
        }
    }

    /// <summary>
    /// Creates an Order. Requires a Customer session having that order, or any Employee belonging to the Group that is creating the Order.
    /// </summary>
    [HttpPost("orders/")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
        {
            if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var customerId))
                return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

            var command = new CreateOrderCommand(customerId, null, request.LocationId, request.CustomerComment, request.IsDelivery, request.PaymentType, request.OrderItems);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<CreateOrderResponse>(result)),
                errors => Problem(errors));
        }
        else
        {
            if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
                return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.NotFound });

            var command = new CreateOrderCommand(null, employeeId, request.LocationId, request.CustomerComment, request.IsDelivery, request.PaymentType, request.OrderItems);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<CreateOrderResponse>(result)),
                errors => Problem(errors));
        }
    }

    /// <summary>
    /// Confirms the Order specified by id. Requires a Customer session having that order.
    /// </summary>
    [HttpPost("orders/{id}/confirm")]
    public async Task<IActionResult> OrderConfirmByCustomer(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is not null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });
        
        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var customerId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

        var command = new ConfirmOrderCommand(id, customerId, null);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Confirms the Order specified by id. Requires an Employee session of a Group that took the Order. 
    /// </summary>
    [HttpPost("orders/{id}/accept")]
    public async Task<IActionResult> OrderConfirmByEmployee(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden});

        var command = new ConfirmOrderCommand(id, null, employeeId);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Marks the Order as completed and notifies the Courier (if applicable) and Customer that the Order can be taken away. Requires an Employee session of a Group that took the Order. 
    /// </summary>
    [HttpPost("orders/{id}/complete")]
    public async Task<IActionResult> OrderCompleteByEmployee(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        var command = new CompleteOrderCommand(id, employeeId);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Confirms an external payment, whether through cash or other measures.
    /// The Customer will be notified about the transaction completion.
    /// Requires an Employee session of a Group that took the Order. 
    /// </summary>
    [HttpPost("orders/{id}/manualpay")]
    public async Task<IActionResult> InitiateOrderPaymentByEmployee(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        var command = new InitiateOrderPaymentCommand(id, null, employeeId, PaymentType.Cash.ToString());
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<InitiateOrderPaymentResponse>(result)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Initiates a Payment for the Order specified by id. 
    /// The Customer will be notified about the transaction and will be provided with instructions on how to complete it. 
    /// Requires a Customer session having that order or an Employee session of a Group that took the Order.
    /// Returns a transaction code for the implemented payment device/service.
    /// </summary>
    [HttpPost("orders/{id}/pay")]
    public async Task<IActionResult> InitiateOrderPaymentByCustomer(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is not null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var customerId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

        var command = new InitiateOrderPaymentCommand(id, customerId, null, PaymentType.BankTransfer.ToString());
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<InitiateOrderPaymentResponse>(result)),
            errors => Problem(errors));
    }


    /// <summary>
    /// Creates an invoice for the order if the invoice payment option was chosen.
    /// Marks the order as paid. 
    /// The Customer will be sent the invoice to his email. 
    /// Requires an Employee session of a Group that took the Order.
    /// </summary>
    [HttpPost("orders/{id}/createinvoice")]
    public async Task<IActionResult> GetOrderInvoice(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        var command = new GetOrderInvoiceCommand(id, employeeId);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetOrderInvoiceResponse>(result)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Rates a single Product in an Order. Required a session of a Customer who took that order.
    /// </summary>
    [HttpPost("order/{id}/rate/{orderitemindex}/{ratingNumber}")]
    public async Task<IActionResult> RateProduct(int id, int orderitemindex, decimal ratingNumber)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is not null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var customerId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Customer.Unauthorized });

        var command = new RateOrderProductCommand(id, orderitemindex, customerId, ratingNumber);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<RateOrderProductResponse>(result)),
            errors => Problem(errors));
    }
}
