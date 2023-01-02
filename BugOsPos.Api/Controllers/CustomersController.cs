using BugOsPos.Application.Authentication.Commands.CustomerRegister;
using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Application.Customers;
using BugOsPos.Contracts.CustomerAuthentication;
using BugOsPos.Contracts.Customers;
using BugOsPos.Domain.Common.ErrorsCollection;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugOsPos.Api.Controllers;

public class CustomersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public CustomersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("customers/signup")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(CustomerRegisterRequest request)
    {
        var command = _mapper.Map<CustomerRegisterCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CustomerAuthenticationResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("customers/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(CustomerLoginRequest request)
    {
        var command = _mapper.Map<CustomerLoginQuery>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CustomerAuthenticationResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet("customers/{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCustomer([FromRoute] GetCustomerByIdRequest request)
    {
        var command = _mapper.Map<GetCustomerByIdQuery>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetCustomerByIdResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPut("customers/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequest request)
    {
        var customerIdString = GetClaimValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(customerIdString, out var customerId))
            return Problem(new() { Errors.Customer.Unauthorized });

        if (customerId != id)
            return Problem(new() { Errors.Customer.Unauthorized });
        
        var command = _mapper.Map<UpdateCustomerCommand>((request, id));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<UpdateCustomerResponse>(result)),
            errors => Problem(errors));
    }
}
