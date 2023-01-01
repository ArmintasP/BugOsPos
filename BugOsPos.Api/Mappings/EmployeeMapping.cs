using BugOsPos.Application.Authentication.Commands.EmployeeRegister;
using BugOsPos.Application.Authentication.Queries.EmployeeLogin;
using BugOsPos.Application.Employees;
using BugOsPos.Contracts.Common;
using BugOsPos.Contracts.EmployeeAuthentication;
using BugOsPos.Contracts.Employees;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.ShiftAggregate;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class EmployeeMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Employee employee, List<Shift> shifts), EmployeeSection>()
            .Map(dest => dest, src => src.employee)
            .Map(dest => dest.Id, src => src.employee.Id.Value)
            .Map(dest => dest.FranchiseId, src => src.employee.FranchiseId.Value)
            .Map(dest => dest.Roles, src => src.employee.Roles.Select(role => role.ToString()))
            .Map(dest => dest.Rating, src => src.employee.Rating.Value)
            .Map(dest => dest.Shifts, src => src.shifts);

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

        config.NewConfig<ShiftSectionRequest, Application.Employees.ShiftSectionCommand>();
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

        config.NewConfig<CourierOrdersResult, CourierOrdersResponse>()
            .Map(dest => dest.Orders, src => src.orders);

        config.NewConfig<Order, OrderSection>()
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.LocationId, src => src.LocationId.Value)
            .Map(dest => dest.CustomerId, src => src.CustomerId.Get())
            .Map(dest => dest.CashierId, src => src.CashierId.Get())
            .Map(dest => dest.CourierId, src => src.CourierId.Get())
            .Map(dest => dest.PaymentId, src => src.Payment.GetId());

        config.NewConfig<OrderItem, OrderItemSection>()
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.DiscountId, src => src.DiscountId.Get())
            .Map(dest => dest.ProductId, src => src.ProductId.Get());


    }
}
