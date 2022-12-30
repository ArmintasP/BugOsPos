using BugOsPos.Api.Attributes;
using BugOsPos.Application.Authentication.Commands.Login;
using BugOsPos.Application.Authentication.Commands.Register;
using BugOsPos.Contracts.Authentication;
using BugOsPos.Domain.EmployeeAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Api.Controllers;

[ApiController]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public AuthenticationController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("customers/signup")]
    public async Task<IActionResult> Register(CustomerRegisterRequest request)
    {
        var command = _mapper.Map<CustomerRegisterCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CustomerAuthenticationResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("customers/login")]
    public async Task<IActionResult> Login(CustomerLoginRequest request)
    {
        var command = _mapper.Map<CustomerLoginCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CustomerAuthenticationResponse>(result)),
            errors => Problem(errors));
    }
}
