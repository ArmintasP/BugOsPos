using BugOsPos.Api.Attributes;
using BugOsPos.Application.Groups;
using BugOsPos.Contracts.Groups;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Api.Controllers;

public sealed class GroupsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public GroupsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Updates a Group specified by id. Requires a session of a Manager belonging to the Franchise. 
    /// </summary>
    [HttpGet("groups/{id}")]
    public async Task<IActionResult> GetGroupById(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.FranchiseIdMissing });
        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.InvalidFranchiseId });

        var query = new GetGroupByIdQuery(id, franchiseId);
        var result = await _mediator.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<GetGroupByIdResponse>(result)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Updates a Group specified by id. Requires a session of a Manager belonging to the Franchise. 
    /// </summary>
    [HttpPut("groups/{id}")]
    [AuthorizeRoles(EmployeeRole.Manager)]
    public async Task<IActionResult> UpdateGroup(int id, string name, string description)
    {
        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.FranchiseIdMissing });

        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.InvalidFranchiseId });

        var command = new UpdateGroupCommand(id, franchiseId, name, description);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<UpdateGroupResponse>(result)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Gets the associated Employees. Requires a session of Employee belonging to the Franchise associated with that Group.
    /// </summary>
    [HttpGet("groups/{id}/employees")]
    public async Task<IActionResult> GetGroupEmployees(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.FranchiseIdMissing });

        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.InvalidFranchiseId });

        var query = new GetGroupEmployeesQuery(id, franchiseId);
        var result = await _mediator.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<GetGroupEmployeesResponse>(result)),
            errors => Problem(errors));
    }
}
