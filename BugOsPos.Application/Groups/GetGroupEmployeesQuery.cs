using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;
using BugOsPos.Domain.ShiftAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Groups;

public sealed record GetGroupEmployeesQuery(
    int Id,
    int FranchiseId) : IRequest<ErrorOr<GetGroupEmployeesResult>>;

public sealed record GetGroupEmployeesResult(List<(Employee, List<Shift>)> Employees);

public sealed class GetGroupEmployeesValidator : AbstractValidator<GetGroupEmployeesQuery>
{
    public GetGroupEmployeesValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FranchiseId).NotEmpty();
    }
}

public sealed class GetGroupEmployeesQueryHandler : IRequestHandler<GetGroupEmployeesQuery, ErrorOr<GetGroupEmployeesResult>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IGroupRepository _groupRepository;

    public GetGroupEmployeesQueryHandler(
        IEmployeeRepository employeeRepository,
        IShiftRepository shiftRepository,
        IGroupRepository groupRepository)
    {
        _employeeRepository = employeeRepository;
        _shiftRepository = shiftRepository;
        _groupRepository = groupRepository;
    }

    public async Task<ErrorOr<GetGroupEmployeesResult>> Handle(GetGroupEmployeesQuery request, CancellationToken cancellationToken)
    {
        var groupId = GroupId.New(request.Id);
        var franchiseId = FranchiseId.New(request.FranchiseId);

        if (await _groupRepository.GetGroupById(groupId) is not Group group ||
            group.FranchiseId != franchiseId)
        {
            return Domain.Common.ErrorsCollection.Errors.Group.BadFranchiseId;
        }
        
        var employees = await _employeeRepository.GetEmployeesByGroupId(groupId);

        var result = new List<(Employee, List<Shift>)>();
        foreach (var employee in employees)
        {
            var shifts = await _shiftRepository.GetShiftsByEmployeeId(employee.Id);
            result.Add((employee, shifts.ToList()));
        }

        return new GetGroupEmployeesResult(result);
    }
}

