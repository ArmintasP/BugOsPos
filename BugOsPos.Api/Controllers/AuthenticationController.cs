using BugOsPos.Application.Authentication.Commands.Register;
using BugOsPos.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Api.Controllers;
[ApiController]
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
    public IActionResult Login(CustomerLoginRequest request)
    {
        return Ok(request);
    }
}
