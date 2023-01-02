using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Contracts.Franchises;
public sealed record CreateProductForFranchiseRequest(
    string Name,
    decimal PriceBeforeTaxes,
    decimal Taxes,
    int EmployeeId,
    int? DiscountId,
    int CategoryId,
    int Quantity);
