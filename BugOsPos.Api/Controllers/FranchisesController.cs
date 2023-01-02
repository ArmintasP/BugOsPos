using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BugOsPos.Application.Franchises;
using BugOsPos.Contracts.Franchises;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Infrastructure.Authentication;

namespace BugOsPos.Api.Controllers;

public sealed class FranchisesController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public FranchisesController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("franchises/{id}")]
    public async Task<IActionResult> GetFranchise(int id)
    {
        var command = new GetFranchiseByIdQuery(id);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetFranchiseByIdResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("franchises")]
    public async Task<IActionResult> CreateFranchise(CreateFranchiseCommand request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        var result = await _mediator.Send(request);

        return result.Match(
            result => Ok(_mapper.Map<CreateFranchiseResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet("franchises/{id}/groups")]
    public async Task<IActionResult> GetGroups(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Errors.Authentication.FranchiseIdMissing });
        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Errors.Authentication.InvalidFranchiseId });

        var command = new GetFranchiseGroupsByIdQuery(id);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetFranchiseGroupsByIdResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet("franchises/{id}/employees")]
    public async Task<IActionResult> GetEmployees(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Errors.Authentication.FranchiseIdMissing });
        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Errors.Authentication.InvalidFranchiseId });

        var command = new GetFranchiseEmployeesByIdQuery(id);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetFranchiseEmployeesByIdResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("franchises/{id}/products")]
    public async Task<IActionResult> CreateProductForFranchise(int id, CreateProductForFranchiseRequest request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        var command = _mapper.Map<CreateProductForFranchiseCommand>((request, id));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CreateProductForFranchiseResponse>(result)),
            errors => Problem(errors));
    }
}
