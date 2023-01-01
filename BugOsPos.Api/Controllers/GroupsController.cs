using BugOsPos.Application.Groups;
using BugOsPos.Contracts.Groups;
using BugOsPos.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Api.Controllers;

[ApiController]
public sealed  class GroupsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public GroupsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("groups/{id}")]

    public async Task<IActionResult> GetGroupById(int id)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Employee.Forbidden });

        if (GetClaimValue(JwtSettings.FranchiseClaim) is not string franchiseIdString)
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.FranchiseIdMissing });
        if (!int.TryParse(franchiseIdString, out var franchiseId))
            return Problem(new() { Domain.Common.ErrorsCollection.Errors.Authentication.InvalidFranchiseId });
        
        var query =  new GetGroupByIdQuery(id, franchiseId);
        var result = await _mediator.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<GetGroupByIdResponse>(result)),
            errors => Problem(errors));
    }
}
