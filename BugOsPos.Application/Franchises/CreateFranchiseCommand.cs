using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;

namespace BugOsPos.Application.Franchises;
public sealed record CreateFranchiseCommand(
    string Name,
    string PhoneNumber,
    string Email,
    int EmployeeId) : IRequest<ErrorOr<CreateFranchiseResult>>;

public sealed record CreateFranchiseResult(Franchise Franchise, Employee Employee);

public sealed class CreateFranchiseValidator : AbstractValidator<CreateFranchiseCommand>
{
    public CreateFranchiseValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
    }
}

public sealed class CreateFranchiseCommandHandler : IRequestHandler<CreateFranchiseCommand, ErrorOr<CreateFranchiseResult>>
{
    private readonly IFranchiseRepository _franchiseRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateFranchiseCommandHandler(IFranchiseRepository franchiseRepository, IEmployeeRepository employeeRepository)
    {
        _franchiseRepository = franchiseRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<CreateFranchiseResult>> Handle(CreateFranchiseCommand request, CancellationToken cancellationToken)
    {
        var franchise = Franchise.New(
            _franchiseRepository.NextIdentity(),
            request.Email,
            request.Name,
            request.PhoneNumber);

        var employee = await _employeeRepository.GetEmployeeById(EmployeeId.New(request.EmployeeId));
        
        if( employee is null)
        {
            return Errors.Employee.NotFound;
        }

        employee = Employee.New(
            employee.Id,
            employee.EmployeeCode,
            employee.Password,
            employee.Salt,
            franchise.Id,
            employee.GroupId,
            employee.ReadAccess,
            employee.Email,
            employee.Name,
            employee.Surname,
            employee.PhoneNumber,
            employee.Address,
            employee.BankAccount,
            employee.Employment,
            employee.Roles,
            employee.DateOfBirth) ;

        await _franchiseRepository.Add(franchise);

        await _employeeRepository.Update(employee);

        return new CreateFranchiseResult(franchise,employee);
    }
}
