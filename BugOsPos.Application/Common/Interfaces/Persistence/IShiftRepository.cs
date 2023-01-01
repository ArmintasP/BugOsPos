using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.ShiftAggregate;
using BugOsPos.Domain.ShiftAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IShiftRepository
{
    ShiftId NextIdentity();
    Task<Shift?> GetShiftById(ShiftId id);
    Task<IEnumerable<Shift>> GetShiftsByEmployeeId(EmployeeId id);
    Task Add(Shift shift);
}