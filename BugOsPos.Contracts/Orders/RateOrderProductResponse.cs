namespace BugOsPos.Contracts.Orders;

public sealed record RateOrderProductResponse(
    int ProductId,
    decimal Rating,
    int NumberOfRatings);
