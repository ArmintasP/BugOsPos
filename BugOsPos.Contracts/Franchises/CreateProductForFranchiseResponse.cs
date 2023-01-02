namespace BugOsPos.Contracts.Franchises;
public sealed record CreateProductForFranchiseResponse(
    int Id,
    string Name,
    int FranchiseId,
    decimal PriceBeforeTaxes,
    decimal Taxes,
    int Quantity);

