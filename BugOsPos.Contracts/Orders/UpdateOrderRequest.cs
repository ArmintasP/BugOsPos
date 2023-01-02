namespace BugOsPos.Contracts.Orders;

public sealed record UpdateOrderRequest(
    int LocationId,
    string? CustomerComment,
    bool IsDelivery,
    string PaymentType);
    