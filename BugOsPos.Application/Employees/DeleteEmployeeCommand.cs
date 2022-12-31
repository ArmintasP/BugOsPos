using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Employees;

public sealed record DeleteEmployeeCommand(int Id, int FranchiseId) : IRequest<ErrorOr<DeleteEmployeeResult>>;

public sealed record DeleteEmployeeResult();

public sealed class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FranchiseId).NotEmpty();
    }
}

public sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ErrorOr<DeleteEmployeeResult>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<DeleteEmployeeResult>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeById(EmployeeId.New(request.Id));
        if (employee is null)
            return Errors.Employee.NotFound;

        if (employee.FranchiseId != FranchiseId.New(request.FranchiseId))
            return Errors.Employee.NotFound;

        await _employeeRepository.Delete(employee.Id);
        return new DeleteEmployeeResult();
    }
}
