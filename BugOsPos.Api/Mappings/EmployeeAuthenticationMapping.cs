using BugOsPos.Application.Authentication.Commands.EmployeeRegister;
using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Application.Authentication.Queries.EmployeeLogin;
using BugOsPos.Contracts.EmployeeAuthentication;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class EmployeeAuthenticationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(EmployeeRegisterRequest request, int franchiseId), EmployeeRegisterCommand>()
            .Map(dest => dest.FranchiseId, src => src.franchiseId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<EmployeeRegisterResult, EmployeeRegisterResponse>()
            .Map(dest => dest, src => src.Employee)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Id, src => src.Employee.Id.Value)
            .Map(dest => dest.FranchiseId, src => src.Employee.FranchiseId.Value)
            .Map(dest => dest.Roles, src => src.Employee.Roles.Select(role => role.ToString()))
            .Map(dest => dest.Rating, src => 0);

        config.NewConfig<EmployeeLoginRequest, EmployeeLoginQuery>();
        config.NewConfig<EmployeeLoginResult, EmployeeLoginResponse>();
    }
}
