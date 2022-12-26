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
            .Map(dest => dest, src => src.customer)
            .Map(dest => dest.Token, src => src.Token);

        config.NewConfig<CustomerRegisterRequest, CustomerRegisterCommand>();
    }
}
