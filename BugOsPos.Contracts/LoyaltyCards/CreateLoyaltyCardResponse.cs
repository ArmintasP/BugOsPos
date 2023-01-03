using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Contracts.LoyaltyCards;
public sealed record CreateLoyaltyCardResponse(
    int LoyaltyCardId,
    int CustomerId,
    string LoyaltyCardCode);
