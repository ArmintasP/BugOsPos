using BugOsPos.Api.Attributes;
using BugOsPos.Application.Authentication.Commands.EmployeeRegister;
using BugOsPos.Application.Authentication.Queries.EmployeeLogin;
using BugOsPos.Application.Employees;
using BugOsPos.Contracts.EmployeeAuthentication;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    //[HttpPut("employees/{id}")]
    //public async Task<IActionResult> Update(int id, EmployeeUpdateRequest request)
    //{
    //    if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var employeeId))
    //        return Problem(new() { Errors.Employee.Forbidden });

    //    if (employeeId != id)
    //        return Problem(new() { Errors.Employee.Forbidden });

    //    if (GetClaimValue(JwtSettings.EployeeClaim) is null)
    //        return Problem(new() { Errors.Employee.Forbidden });

    //    var query = _mapper.Map<EmployeeUpdateQuery>((id, request));
    //    var result = await _mediator.Send(query);

    //    return result.Match(
    //        result => Ok(_mapper.Map<EmployeeUpdateResponse>(result)),
    //        errors => Problem(errors));
    //}

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

    [HttpDelete("employees/{id}")]
    [AuthorizeRoles(EmployeeRole.Manager)]
    public async Task<IActionResult> Delete(int id)
    {
        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Errors.Authentication.FranchiseIdMissing });
        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Errors.Authentication.InvalidFranchiseId });

        if (!int.TryParse(GetClaimValue(ClaimTypes.NameIdentifier), out var managerId))
            return Problem(new() { Errors.Employee.Forbidden });
        if (managerId == id)
            return Problem(new() { Errors.Employee.Forbidden });

        var query = new DeleteEmployeeCommand(id, franchiseId);
        var result = await _mediator.Send(query);

        return result.Match(
            result => Ok(),
            errors => Problem(errors));
    }
}
