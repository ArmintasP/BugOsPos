namespace BugOsPos.Contracts.Orders;
public sealed record InitiateOrderPaymentResponse(
    int Id,
    string Type, 
    DateTime CreatedAt);