using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Contracts.LoyaltyCards;
public record AddLoyaltyCardDiscountsRequest(IEnumerable<int> DiscountIds);
