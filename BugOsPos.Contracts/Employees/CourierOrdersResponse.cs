namespace BugOsPos.Contracts.Employees;
public sealed record CourierOrdersResponse(List<OrderSection> Orders);

public sealed record OrderSection(
    int Id,
    int? CustomerId,
    int? CashierId,
    int? CourierId,
    int? LocationId,
    int? PaymentId,
    decimal? Tips,
    string Status,
    decimal TotalPrice,
    string? CustomerComment,
    TimeSpan? EstimatedTime,
    List<OrderItemSection> OrderItems);

public sealed record OrderItemSection(
    int Id,
    int DiscountId,
    int ProductId,
    int Quantity,
    string Status);