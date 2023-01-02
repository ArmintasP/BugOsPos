using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.ProductAggregate;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.CategoryAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;

namespace BugOsPos.Application.Franchises;
public sealed record CreateProductForFranchiseCommand(
    int Id,
    string Name,
    decimal PriceBeforeTaxes,
    decimal Taxes,
    int EmployeeId,
    int? DiscountId,
    int CategoryId,
    int Quantity) : IRequest<ErrorOr<CreateProductForFranchiseResult>>;

public sealed record CreateProductForFranchiseResult(Product Product);

public sealed class CreateProductForFranchiseValidator : AbstractValidator<CreateProductForFranchiseCommand>
{
    public CreateProductForFranchiseValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.PriceBeforeTaxes).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Taxes).NotEmpty().GreaterThan(0);
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
    }
}

public sealed class CreateProductForFranchiseCommandHandler : IRequestHandler<CreateProductForFranchiseCommand, ErrorOr<CreateProductForFranchiseResult>>
{
    private readonly IFranchiseRepository _franchiseRepository;
    private readonly IProductRepository _productRepository;

    public CreateProductForFranchiseCommandHandler(IFranchiseRepository franchiseRepository, IProductRepository productRepository)
    {
        _franchiseRepository = franchiseRepository;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<CreateProductForFranchiseResult>> Handle(CreateProductForFranchiseCommand request, CancellationToken cancellationToken)
    {
        var franchise = await _franchiseRepository.GetFranchiseById(FranchiseId.New(request.Id));

        if (franchise is null)
            return Errors.Franchise.NotFound;

        var product = Product.NewProduct(
            _productRepository.NextIdentity(),
            franchise.Id,
            EmployeeId.New(request.EmployeeId),
            DiscountId.New(request.DiscountId ?? 0),
            CategoryId.New(request.CategoryId),
            request.Name,
            request.PriceBeforeTaxes,
            request.Taxes,
            request.Quantity);

        await _productRepository.Add(product);

        return new CreateProductForFranchiseResult(product);
    }
}
