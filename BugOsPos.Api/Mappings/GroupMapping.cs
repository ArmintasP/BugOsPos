using BugOsPos.Application.Groups;
using BugOsPos.Contracts.Common;
using BugOsPos.Contracts.Groups;
using BugOsPos.Domain.GroupAggregate;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class GroupMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Group, GroupSection>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.FranchiseId, src => src.FranchiseId.Value);

        config.NewConfig<GetGroupByIdResult, GetGroupByIdResponse>()
            .Map(dest => dest.Group, src => src.Group);
    }
}