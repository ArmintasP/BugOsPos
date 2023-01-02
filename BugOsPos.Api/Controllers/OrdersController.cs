using BugOsPos.Application.Employees;
using BugOsPos.Contracts.Orders;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
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
}
