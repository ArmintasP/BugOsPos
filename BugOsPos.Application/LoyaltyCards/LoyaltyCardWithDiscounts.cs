using BugOsPos.Domain.DiscountAggregate;
using BugOsPos.Domain.LoyaltyCardAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Application.LoyaltyCards;
public sealed record LoyaltyCardWithDiscounts(LoyaltyCard loyaltyCard, List<Discount> discounts);
