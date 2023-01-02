using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.FranchiseAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate;
using BugOsPos.Domain.EmployeeAggregate;

namespace BugOsPos.Application.Franchises;

public sealed record GetFranchiseByIdQuery(int Id) : IRequest<ErrorOr<GetFranchiseByIdResult>>;

public sealed record GetFranchiseByIdResult(Franchise Franchise, List<Product> Products, List<Employee> Employees);

public sealed class GetFranchiseByIdValidator : AbstractValidator<GetFranchiseByIdQuery>
{
    public GetFranchiseByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetFranchiseByIdQueryHandler : IRequestHandler<GetFranchiseByIdQuery, ErrorOr<GetFranchiseByIdResult>>
{
    private readonly IFranchiseRepository _franchiseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetFranchiseByIdQueryHandler(IFranchiseRepository franchiseRepository, IProductRepository productRepository, IEmployeeRepository employeeRepository)
    {
        _franchiseRepository = franchiseRepository;
        _productRepository = productRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<GetFranchiseByIdResult>> Handle(GetFranchiseByIdQuery request, CancellationToken cancellationToken)
    {
        var franchise = await _franchiseRepository.GetFranchiseById(FranchiseId.New(request.Id));
        if (franchise is null)
            return Errors.Franchise.NotFound;

        var products = await _productRepository.GetProductsByFranchiseId(FranchiseId.New(request.Id));

        var employees = await _employeeRepository.GetEmployeesByFranchiseId(FranchiseId.New(request.Id));

        return new GetFranchiseByIdResult(franchise, products.ToList(), employees.ToList());
    }
}
