using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Contracts.LoyaltyCards;
public sealed record GetLoyaltyCardByIdResponse(
    int LoyaltyCardId, 
    int CustomerId, 
    string LoyaltyCardCode, 
    List<LoyaltyCardDiscount> Discounts);


public sealed record LoyaltyCardDiscount(
    decimal Amount,
    int DiscountType,
    DateTime FromDateTime,
    DateTime EndDateTime);