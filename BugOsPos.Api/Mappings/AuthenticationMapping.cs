using BugOsPos.Application.Authentication.Commands.Register;
using BugOsPos.Application.Authentication.Common;
using BugOsPos.Contracts.Authentication;
using Mapster;

namespace BugOsPos.Api.Mappings;

public class AuthenticationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, CustomerAuthenticationResponse>()
            .Map(dest => dest.Id, src => src.Customer.Id.Value)
            .Map(dest => dest.FranchiseId, src => src.Customer.FranchiseId.Value)
            .Map(dest => dest, src => src.Customer)
            .Map(dest => dest.Token, src => src.Token);

        config.NewConfig<CustomerRegisterRequest, CustomerRegisterCommand>();
    }
}
