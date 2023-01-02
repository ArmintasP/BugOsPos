using BugOsPos.Application.Franchises;
using BugOsPos.Contracts.Franchises;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class FranchiseMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetFranchiseByIdResult, GetFranchiseByIdResponse>()
            .Map(dest => dest.Email, src => src.Franchise.Email)
            .Map(dest => dest.Name, src => src.Franchise.Name)
            .Map(dest => dest.PhoneNumber, src => src.Franchise.PhoneNumber)
            .Map(dest => dest.Products, src => src.Products)
            .Map(dest => dest.Employees, src => src.Employees);

        config.NewConfig<Domain.ProductAggregate.Product, Contracts.Franchises.Product>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.FranchiseId, src => src.FranchiseId.Value);

        config.NewConfig<Domain.EmployeeAggregate.Employee, Employee>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Rating, src => src.Rating.Value)
            .Map(dest => dest.FranchiseId, src => src.FranchiseId.Value);

        config.NewConfig<CreateFranchiseResult, CreateFranchiseResponse>()
            .Map(dest => dest.Employee, src => src.Employee)
            .Map(dest => dest.Employee.Id, src => src.Employee.Id.Value)
            .Map(dest => dest.Id, src => src.Franchise.Id.Value)
            .Map(dest => dest.Name, src => src.Franchise.Name)
            .Map(dest => dest.Email, src => src.Franchise.Email);

        config.NewConfig<CreateProductForFranchiseResult, CreateProductForFranchiseResponse>()
            .Map(dest => dest, src => src.Product)
            .Map(dest => dest.Id, src => src.Product.Id.Value)
            .Map(dest => dest.FranchiseId, src => src.Product.FranchiseId.Value);

        config.NewConfig<(CreateProductForFranchiseRequest request, int id), CreateProductForFranchiseCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest, src => src.request);

        config.NewConfig<GetFranchiseEmployeesByIdResult, GetFranchiseEmployeesByIdResponse>()
          .Map(dest => dest.Employees, src => src.Employees);

        config.NewConfig<GetFranchiseGroupsByIdResult, GetFranchiseGroupsByIdResponse>()
            .Map(dest => dest.Groups, src => src.Groups);
    }
}
