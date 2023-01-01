using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.ShiftAggregate;
using BugOsPos.Domain.ShiftAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class ShiftRepository : IShiftRepository
{
    private static readonly List<Shift> _shifts = PrefilledData.SampleShifts();
    private int _nextId = _shifts.Count + 1;
        
    public ShiftId NextIdentity()
    {
        return ShiftId.New(_nextId);
    }
    
    public Task<Shift?> GetShiftById(ShiftId id)
    {
        var shift = _shifts.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(shift);
    }

    public Task Add(Shift shift)
    {
        _shifts.Add(shift);
        _nextId++;
        return Task.CompletedTask;
    }
}
