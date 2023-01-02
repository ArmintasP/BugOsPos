using BugOsPos.Application.Common.Interfaces.Persistence;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate;

namespace BugOsPos.Application.Franchises;

public sealed record GetFranchiseEmployeesByIdQuery(int Id) : IRequest<ErrorOr<GetFranchiseEmployeesByIdResult>>;

public sealed record GetFranchiseEmployeesByIdResult(List<Employee> Employees);

public sealed class GetFranchiseEmployeesByIdValidator : AbstractValidator<GetFranchiseEmployeesByIdQuery>
{
    public GetFranchiseEmployeesByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetFranchiseEmployeesByIdQueryHandler : IRequestHandler<GetFranchiseEmployeesByIdQuery, ErrorOr<GetFranchiseEmployeesByIdResult>>
{
    private readonly IFranchiseRepository _franchiseRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetFranchiseEmployeesByIdQueryHandler(IFranchiseRepository franchiseRepository, IEmployeeRepository employeeRepository)
    {
        _franchiseRepository = franchiseRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<GetFranchiseEmployeesByIdResult>> Handle(GetFranchiseEmployeesByIdQuery request, CancellationToken cancellationToken)
    {
        var franchise = await _franchiseRepository.GetFranchiseById(FranchiseId.New(request.Id));
        if (franchise is null)
            return Errors.Franchise.NotFound;

        var employees = await _employeeRepository.GetEmployeesByFranchiseId(FranchiseId.New(request.Id));

        return new GetFranchiseEmployeesByIdResult(employees.ToList());
    }
}
