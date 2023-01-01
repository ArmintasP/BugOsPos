using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Groups;

public sealed record UpdateGroupCommand(
    int Id,
    int FranchiseId,
    string Name,
    string Description) : IRequest<ErrorOr<UpdateGroupResult>>;

public sealed record UpdateGroupResult(Group Group);

public sealed class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

public sealed class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, ErrorOr<UpdateGroupResult>>
{
    private readonly IGroupRepository _groupRepository;

    public UpdateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<ErrorOr<UpdateGroupResult>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetGroupById(GroupId.New(request.Id));
        if (group is null)
            return Domain.Common.ErrorsCollection.Errors.Group.NonExistentId;

        if (group.FranchiseId != FranchiseId.New(request.FranchiseId))
            return Domain.Common.ErrorsCollection.Errors.Group.BadFranchiseId;

        group = Group.New(group.Id, group.FranchiseId, request.Name, request.Description);
        await _groupRepository.Update(group);
        
        return new UpdateGroupResult(group);
    }
}