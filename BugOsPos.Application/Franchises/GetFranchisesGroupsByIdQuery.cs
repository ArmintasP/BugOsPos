using BugOsPos.Application.Common.Interfaces.Persistence;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;

namespace BugOsPos.Application.Franchises;

public sealed record GetFranchiseGroupsByIdQuery(int Id) : IRequest<ErrorOr<GetFranchiseGroupsByIdResult>>;

public sealed record GetFranchiseGroupsByIdResult(List<Group> Groups);

public sealed class GetFranchiseGroupsByIdValidator : AbstractValidator<GetFranchiseGroupsByIdQuery>
{
    public GetFranchiseGroupsByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetFranchiseGroupsByIdQueryHandler : IRequestHandler<GetFranchiseGroupsByIdQuery, ErrorOr<GetFranchiseGroupsByIdResult>>
{
    private readonly IFranchiseRepository _franchiseRepository;
    private readonly IGroupRepository _groupRepository;

    public GetFranchiseGroupsByIdQueryHandler(IFranchiseRepository franchiseRepository, IGroupRepository groupRepository)
    {
        _franchiseRepository = franchiseRepository;
        _groupRepository = groupRepository;
    }

    public async Task<ErrorOr<GetFranchiseGroupsByIdResult>> Handle(GetFranchiseGroupsByIdQuery request, CancellationToken cancellationToken)
    {
        var franchise = await _franchiseRepository.GetFranchiseById(FranchiseId.New(request.Id));
        if (franchise is null)
            return Errors.Franchise.NotFound;


        var groups = await _groupRepository.GetGroupsByFranchiseId(FranchiseId.New(request.Id));

        return new GetFranchiseGroupsByIdResult(groups.ToList());
    }
}

