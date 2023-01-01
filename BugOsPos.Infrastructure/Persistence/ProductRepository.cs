using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.ProductAggregate;
using BugOsPos.Domain.ProductAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;
public sealed class ProductRepository : IProductRepository
{
    private static readonly List<Product> _products = PrefilledData.SampleProducts();
    private int _nextId = _products.Count + 1;

    public ProductId NextIdentity()
    {
        return ProductId.New(_nextId);
    }

    public Task Add(Product product)
    {
        _products.Add(product);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Product?> GetProductById(ProductId id)
    {
        var product = _products.SingleOrDefault(
            e => e.Id == id);
        
        return Task.FromResult(product);
    }

    public Task Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        _products[index] = product;
        return Task.CompletedTask;
    }
}
