using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate;
using BugOsPos.Domain.ProductAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    ProductId NextIdentity();
    Task<Product?> GetProductById(ProductId id);
    Task<IEnumerable<Product>> GetProductsByFranchiseId(FranchiseId id);
    Task Add(Product product);
    Task Update(Product product);
    Task <IEnumerable<Product>> GetAllProducts();
}
