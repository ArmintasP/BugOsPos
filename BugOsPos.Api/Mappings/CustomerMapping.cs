using BugOsPos.Application.Authentication.Commands.CustomerRegister;
using BugOsPos.Application.Authentication.Common;
using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Application.Customers;
using BugOsPos.Contracts.CustomerAuthentication;
using BugOsPos.Contracts.Customers;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class CustomerMapping : IRegister
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

        config.NewConfig<GetCustomerByIdRequest, GetCustomerByIdQuery>();
        config.NewConfig<GetCustomerByIdResult, GetCustomerByIdResponse>()
            .Map(dest => dest, src => src.Customer)
            .Map(dest => dest.FranchiseId, src => src.Customer.FranchiseId.Value)
            .Map(dest => dest.Id, src => src.Customer.Id.Value);

        config.NewConfig<(UpdateCustomerRequest request, int id), UpdateCustomerCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest, src => src.request);

        config.NewConfig<UpdateCustomerResult, UpdateCustomerResponse>()
            .Map(dest => dest.Id, src => src.Customer.Id.Value)
            .Map(dest => dest, src => src.Customer);
    }
}
