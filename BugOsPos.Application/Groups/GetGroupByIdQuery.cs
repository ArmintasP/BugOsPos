using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Application.Employees;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Groups;

public sealed record GetGroupByIdQuery(int Id, int FranchiseId) : IRequest<ErrorOr<GetGroupResult>>;

public sealed record GetGroupResult(Group Group);

public sealed class GetGroupByIdValidator : AbstractValidator<GetGroupByIdQuery>
{
    public GetGroupByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FranchiseId).NotEmpty();
    }
}

public sealed class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, ErrorOr<GetGroupResult>>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupByIdQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<ErrorOr<GetGroupResult>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var groupId = GroupId.New(request.Id);
        var franchiseId = FranchiseId.New(request.FranchiseId);
        
        if (await _groupRepository.GetGroupById(groupId) is not Group group ||
            group.FranchiseId != franchiseId)
        {
            return Domain.Common.ErrorsCollection.Errors.Group.BadFranchiseId;
        }
        
        return new GetGroupResult(group);
    }
}