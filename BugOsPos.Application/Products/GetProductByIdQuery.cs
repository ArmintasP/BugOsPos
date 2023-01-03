using FluentValidation;
using MediatR;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.ProductAggregate;
using BugOsPos.Domain.ProductAggregate.ValueObjects;
using ErrorOr;
using BugOsPos.Domain.Common.ErrorsCollection;


namespace BugOsPos.Application.Products;

public sealed record GetProductByIdQuery(int Id) : IRequest<ErrorOr<GetProductByIdResult>>;

public sealed record GetProductByIdResult(Product Product);

public sealed class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<GetProductByIdResult>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<GetProductByIdResult>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(ProductId.New(request.Id));
        if (product is null)
            return Errors.Product.NotFound;

        return new GetProductByIdResult(product);
    }
    
}
