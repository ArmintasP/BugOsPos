namespace BugOsPos.Contracts.Common;

public sealed record OrderSection(
    int Id,
    int? CustomerId,
    int? PaymentId,
    int? CashierId,
    int? CourierId,
    int LocationId,
    decimal? Tips,
    decimal TotalPrice,
    string? CustomerComment,
    string Status,
    TimeSpan? EstimatedTime,
    List<OrderItemSection> OrderItems);
