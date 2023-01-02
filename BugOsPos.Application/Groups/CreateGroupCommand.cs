using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Groups;

public sealed record CreateGroupCommand(
    int FranchiseId,
    string Name,
    string Description) : IRequest<ErrorOr<CreateGroupResult>>;

public sealed record CreateGroupResult(Group Group);

public sealed class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupValidator()
    {
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ErrorOr<CreateGroupResult>>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<ErrorOr<CreateGroupResult>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = Group.New(
            _groupRepository.NextIdentity(),
            FranchiseId.New(request.FranchiseId),
            request.Name,
            request.Description);

        await _groupRepository.Add(group);
        return new CreateGroupResult(group);
    }
}