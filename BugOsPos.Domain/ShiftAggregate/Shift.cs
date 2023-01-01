using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate.ValueObjects;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using BugOsPos.Domain.ShiftAggregate.ValueObjects;

namespace BugOsPos.Domain.ShiftAggregate;

public sealed class Shift : AggregateRoot<ShiftId>
{
    public EmployeeId EmployeeId { get; }
    public LocationId LocationId { get; }
    public GroupId? GroupId { get; }
    public DateTime Start { get; }
    public DateTime End { get; }
    
    private Shift(
        ShiftId id,
        EmployeeId employeeId,
        DateTime start,
        DateTime end,
        LocationId locationId,
        GroupId? groupId)
        : base(id)
    {
        EmployeeId = employeeId;
        Start = start;
        End = end;
        LocationId = locationId;
        GroupId = groupId;
    }

    public static Shift New(
        ShiftId id,
        EmployeeId employeeId,
        DateTime start,
        DateTime end,
        LocationId locationId,
        GroupId? groupId)
    {
        return new Shift(
            id,
            employeeId,
            start,
            end,
            locationId,
            groupId);
    }
}
