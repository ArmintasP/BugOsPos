using FluentValidation;
using MediatR;
//using BugOsPos.Infrastructure.Persistence;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.ProductAggregate;
using BugOsPos.Domain.ProductAggregate.ValueObjects;
using ErrorOr;
using BugOsPos.Domain.Common.ErrorsCollection;


namespace BugOsPos.Application.Products;

public sealed record GetProductsQuery() : IRequest<ErrorOr<GetProductsResult>>;

public sealed record GetProductsResult(List<Product> Products);

public sealed class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsValidator()
    {
       // RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ErrorOr<GetProductsResult>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<GetProductsResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        //var products = await _productRepository.GetProductById(_productRepository.NextIdentity());
        var products = await _productRepository.GetAllProducts();
        
        return new GetProductsResult(products.ToList());
    }
}