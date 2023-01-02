namespace BugOsPos.Contracts.Common;

public sealed record OrderItemSection(
    int Id,
    int ProductId,
    int? DiscountId,
    int Quantity,
    string Status);