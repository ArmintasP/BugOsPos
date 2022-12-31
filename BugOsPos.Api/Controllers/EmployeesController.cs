using BugOsPos.Api.Attributes;
using BugOsPos.Application.Authentication.Commands.EmployeeRegister;
using BugOsPos.Application.Authentication.Queries.EmployeeLogin;
using BugOsPos.Contracts.EmployeeAuthentication;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Api.Controllers;

public sealed class EmployeesController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public EmployeesController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("employees/create")]
    [AuthorizeRoles(EmployeeRole.Manager)]
    public async Task<IActionResult> Register(EmployeeRegisterRequest request)
    {
        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Errors.Authentication.FranchiseIdMissing });

        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Errors.Authentication.InvalidFranchiseId });
        
        var command = _mapper.Map<EmployeeRegisterCommand>((request, franchiseId));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<EmployeeRegisterResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("employees/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(EmployeeLoginRequest request)
    {
        var query = _mapper.Map<EmployeeLoginQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<EmployeeLoginResponse>(result)),
            errors => Problem(errors));
    }
}
