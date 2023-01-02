using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.ShiftAggregate;
using BugOsPos.Domain.EmployeeAggregate;

namespace BugOsPos.Application.Locations;

public sealed record GetLocationEmployeesQuery(int Id) : IRequest<ErrorOr<GetLocationEmployeesResult>>;
public sealed record GetLocationEmployeesResult(List<(Employee, List<Shift>)> Employees);

public sealed class GetLocationEmployeesValidator : AbstractValidator<GetLocationEmployeesQuery>
{
    public GetLocationEmployeesValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetLocationEmployeesCommandHandler : IRequestHandler<GetLocationEmployeesQuery, ErrorOr<GetLocationEmployeesResult>>
{
    private readonly IShiftRepository _shiftRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetLocationEmployeesCommandHandler(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
    {
        _shiftRepository = shiftRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<GetLocationEmployeesResult>> Handle(GetLocationEmployeesQuery request, CancellationToken cancellationToken)
    {
        var shifts = await _shiftRepository.GetShiftsByLocationId(LocationId.New(request.Id));
        var employees = await Task.WhenAll(shifts
            .Select(prop => prop.EmployeeId)
            .Distinct()
            .Select(async p => await _employeeRepository.GetEmployeeById(p)));

        var result = new List<(Employee, List<Shift>)>();
        foreach (var employee in employees)
        {
            if (employee is not null)
            {
                var shiftsT = await _shiftRepository.GetShiftsByEmployeeId(employee.Id);
                result.Add((employee, shifts.ToList()));
            }
        }

        return new GetLocationEmployeesResult(result);
    }
}

