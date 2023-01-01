using BugOsPos.Application.Authentication.Commands.EmployeeRegister;
using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Application.Authentication.Queries.EmployeeLogin;
using BugOsPos.Application.Employees;
using BugOsPos.Contracts.EmployeeAuthentication;
using BugOsPos.Contracts.Employees;
using BugOsPos.Domain.ShiftAggregate;
using BugOsPos.Domain.ShiftAggregate.ValueObjects;
using Mapster;
using MediatR;

namespace BugOsPos.Api.Mappings;

public sealed class EmployeeMapping : IRegister
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

        config.NewConfig<ShiftSectionRequest, ShiftSection>();
        config.NewConfig<(int id, EmployeeUpdateRequest request), EmployeeUpdateCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest, src => src.request);


        config.NewConfig<EmployeeUpdateResult, EmployeeUpdateResponse>()
            .Map(dest => dest.Shifts, src => src.Shifts)
            .Map(dest => dest, src => src.Employee);

        config.NewConfig<Shift, ShiftSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.LocationId, src => src.LocationId.Value)
            .Map(dest => dest.Start, src => src.Start)
            .Map(dest => dest.End, src => src.End)
            .IgnoreNonMapped(true);
    }
}
