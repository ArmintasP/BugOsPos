using System;
using System.Collections.Generic;

namespace BugOsPos.Contracts.Products;
public sealed record GetProductByIdResponse(
    int Id,
    string Name,
    double Price,
    int FranchiseId,
    int CategoryId,
    int DiscountId,
    decimal PriceBeforeTaxes,
    decimal Taxes,
    int Quantity,
    bool IsProduct,
    TimeOnly? Duration,
    DateTime? ReservationDate);