using BugOsPos.Domain.ProductAggregate;

namespace BugOsPos.Contracts.Products;
public sealed record GetProductsResponse(List<Product> Products);