using BugOsPos.Contracts.Common;

namespace BugOsPos.Contracts.Employees;
public sealed record CourierOrdersResponse(List<OrderSection> Orders);