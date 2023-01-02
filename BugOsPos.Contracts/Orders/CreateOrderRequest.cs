namespace BugOsPos.Contracts.Orders;

public sealed record CreateOrderRequest(
    int LocationId,
    string? CustomerComment,
    bool IsDelivery,
    string PaymentType,
    List<int> OrderItems);
    