using BugOsPos.Application.Locations;
using BugOsPos.Contracts.Locations;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using BugOsPos.Domain.Common.ErrorsCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Api.Controllers;

[ApiController]
public class LocationsController : ApiController
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

        var command = _mapper.Map<UpdateLocationCommand>((id,request));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<UpdateLocationResponse>(result)),
            errors => Problem(errors));
    }
}
