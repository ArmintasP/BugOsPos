using BugOsPos.Application.Authentication.Commands.CustomerRegister;
using BugOsPos.Application.Authentication.Common;
using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Contracts.CustomerAuthentication;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class CustomerAuthenticationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CustomerAuthenticationResult, CustomerAuthenticationResponse>()
            .Map(dest => dest.Id, src => src.Customer.Id.Value)
            .Map(dest => dest.FranchiseId, src => src.Customer.FranchiseId.Value)
            .Map(dest => dest, src => src.Customer)
            .Map(dest => dest.Token, src => src.Token);

        config.NewConfig<CustomerRegisterRequest, CustomerRegisterCommand>();
        config.NewConfig<CustomerLoginRequest, CustomerLoginQuery>();
    }
}
