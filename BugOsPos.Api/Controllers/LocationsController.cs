using BugOsPos.Application.Locations;
using BugOsPos.Contracts.Locations;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using BugOsPos.Domain.Common.ErrorsCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Api.Controllers;

public sealed class LocationsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public LocationsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("locations/{id}")]
    public async Task<IActionResult> GetLocation(int id)
    {
        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Errors.Authentication.FranchiseIdMissing });

        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Errors.Authentication.InvalidFranchiseId });

        var command = new GetLocationByIdQuery(id);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetLocationByIdResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPut("locations/{id}")]
    public async Task<IActionResult> UpdateLocation(int id, UpdateLocationRequest request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        var command = _mapper.Map<UpdateLocationCommand>((id, request));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<UpdateLocationResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet("locations/{id}/employees")]
    public async Task<IActionResult> GetLocationEmployees(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Errors.Authentication.FranchiseIdMissing });

        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Errors.Authentication.InvalidFranchiseId });

        var query = new GetLocationEmployeesQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<GetLocationEmployeesResponse>(result)),
            errors => Problem(errors));
    }


    [HttpPost("locations")]
    public async Task<IActionResult> CreateLocation(CreateLocationCommand request)
    {
        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Errors.Authentication.FranchiseIdMissing });

        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Errors.Authentication.InvalidFranchiseId });

        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        var command = _mapper.Map<CreateLocationCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CreateLocationResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPut("locations/{id}/rate/{ratingNumber}")]
    public async Task<IActionResult> RateLocation(int id, decimal ratingNumber)
    {
        var command = new CreateLocationRatingCommand(id, ratingNumber);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CreateLocationRatingResponse>(result)),
            errors => Problem(errors));
    }
}
