using BugOsPos.Application.Common.Behaviors;
using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Clock;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;
using Employee = BugOsPos.Domain.EmployeeAggregate.Employee;
using Shift = BugOsPos.Domain.ShiftAggregate.Shift;

namespace BugOsPos.Application.Employees;

public sealed record EmployeeUpdateCommand(
    int Id,
    string Email,
    string NewPassword,
    string Name,
    string Surname,
    string BankAccount,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    int ReadAccess,
    decimal Employment,
    List<ShiftSection> Shifts) : IRequest<ErrorOr<EmployeeUpdateResult>>;

public sealed record ShiftSection(
    int LocationId,
    DateTime Start,
    DateTime End);

public sealed record EmployeeUpdateResult(Employee Employee, List<Shift> Shifts);

public sealed class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdateCommand>
{
    private const int LegalWorkingAge = 18;

    public EmployeeUpdateValidator(IClock clock)
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.BankAccount).NotEmpty();
        RuleFor(x => x.DateOfBirth).GreaterThanOrEqualTo(
            DateOnly.FromDateTime(clock.UtcNow.AddYears(-LegalWorkingAge)));
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Employment)
            .LessThanOrEqualTo(1)
            .GreaterThan(0);
        RuleFor(x => x.Shifts).NotEmpty();
    }
}

public sealed class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand, ErrorOr<EmployeeUpdateResult>>
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IPasswordProvider _passwordProvider;

    public EmployeeUpdateCommandHandler(
        IEmployeeRepository employeeRepository,
        IShiftRepository shiftRepository,
        IPasswordProvider passwordProvider,
        ILocationRepository locationRepository)
    {
        _employeeRepository = employeeRepository;
        _shiftRepository = shiftRepository;
        _passwordProvider = passwordProvider;
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<EmployeeUpdateResult>> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeById(EmployeeId.New(request.Id));
        if (employee is null)
            return Errors.Employee.NotFound;

        foreach (var shiftRequest in request.Shifts)
        {
            var location = await _locationRepository.GetLocationById(LocationId.New(shiftRequest.LocationId));
            if (location is null)
                return Errors.Location.NotFound;
        }

        var (hashedPassword, salt) = _passwordProvider.HashPassword(request.NewPassword);

        employee = Employee.New(
            employee.Id,
            employee.EmployeeCode,
            hashedPassword,
            salt,
            employee.FranchiseId,
            employee.GroupId,
            request.ReadAccess,
            request.Email,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Address,
            request.BankAccount,
            request.Employment,
            employee.Roles,
            request.DateOfBirth);

        await _employeeRepository.Update(employee);

        var shifts = new List<Shift>();
        foreach (var shiftRequest in request.Shifts)
        {
            var shift = Shift.New(
                _shiftRepository.NextIdentity(),
                employee.Id,
                shiftRequest.Start,
                shiftRequest.End,
                LocationId.New(shiftRequest.LocationId),
                employee.GroupId);

            await _shiftRepository.Add(shift);

            shifts.Add(shift);
        }

        return new EmployeeUpdateResult(employee, shifts);
    }
}
