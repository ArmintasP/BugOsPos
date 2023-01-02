using BugOsPos.Contracts.Common;
using BugOsPos.Domain.ShiftAggregate;
using Mapster;

namespace BugOsPos.Api.Mappings;

public class ShiftMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Shift, ShiftSection>()
            .Map(dest => dest.Id, src => src.Id.Get())
            .Map(dest => dest.LocationId, src => src.LocationId.Get());
    }
}
