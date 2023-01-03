using BugOsPos.Api.Attributes;
using BugOsPos.Application.Authentication.Commands.EmployeeRegister;
using BugOsPos.Application.Authentication.Queries.EmployeeLogin;
using BugOsPos.Application.Customers;
using BugOsPos.Application.Employees;
using BugOsPos.Application.LoyaltyCards;
using BugOsPos.Contracts.Customers;
using BugOsPos.Contracts.EmployeeAuthentication;
using BugOsPos.Contracts.Employees;
using BugOsPos.Contracts.LoyaltyCards;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugOsPos.Api.Controllers;

public sealed class LoyaltyCardController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public LoyaltyCardController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpGet("loyaltycard/{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetLoyaltyCard([FromRoute] GetLoyaltyCardByIdRequest request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });
        var command = _mapper.Map<GetLoyaltyCardByIdQuery>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetLoyaltyCardByIdResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("loyaltycard/create")]
    [Authorize]
    public async Task<IActionResult> CreateLoyaltyCard([FromBody] CreateLoyaltyCardRequest request)
    {
        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        var command = _mapper.Map<CreateLoyaltyCardCommand>( request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CreateLoyaltyCardResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("loyaltycard/{id:int}/discounts")]
    [Authorize]
    public async Task<IActionResult> AddDiscounts(int id, AddLoyaltyCardDiscountsRequest request)
    {
        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        var command = _mapper.Map<AddLoyaltyCardDiscountsCommand>((id, request));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<AddLoyaltyCardDiscountsResponse>(result)),
            errors => Problem(errors));
    }
}
