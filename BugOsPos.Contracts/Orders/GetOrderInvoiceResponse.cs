namespace BugOsPos.Contracts.Orders;

public sealed record GetOrderInvoiceResponse
    (int Id,
    string Type,
    DateTime CreatedAt,
    string Invoice);
