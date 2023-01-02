using BugOsPos.Application.Products;
using BugOsPos.Contracts.Products;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class ProductMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetProductByIdResult, GetProductByIdResponse>()
            .Map(dest => dest, src => src.Product)
            .Map(dest => dest.Id, src => src.Product.Id.Get())
            .Map(dest => dest.FranchiseId, src => src.Product.FranchiseId.Get())
            .Map(dest => dest.CategoryId, src => src.Product.CategoryId.Get())
            .Map(dest => dest.DiscountId, src => src.Product.DiscountId.Get());
    }
}