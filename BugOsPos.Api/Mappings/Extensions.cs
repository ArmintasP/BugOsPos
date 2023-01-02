using BugOsPos.Domain.CategoryAggregate.ValueObjects;
using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate.ValueObjects;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyDiscountAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate.ValueObjects;
using BugOsPos.Domain.ShiftAggregate.ValueObjects;

namespace BugOsPos.Api.Mappings;

internal static class Extensions
{
    public static int? Get(this ValueObject? obj)
    {
        if (obj is not ValueObject valueObject)
            return null;

        if (valueObject is EmployeeId employeeId)
            return employeeId.Value;
        if (valueObject is PaymentId paymentId)
            return paymentId.Value;
        if (valueObject is OrderId orderId)
            return orderId.Value;
        if (valueObject is LocationId locationId)
            return locationId.Value;
        if (valueObject is ProductId productId)
            return productId.Value;
        if (valueObject is CategoryId categoryId)
            return categoryId.Value;
        if (valueObject is ShiftId shiftId)
            return shiftId.Value;
        if (valueObject is DiscountId discountId)
            return discountId.Value;
        if (valueObject is CustomerId customerId)
            return customerId.Value;
        if (valueObject is LoyaltyCardId loyaltyCardId)
            return loyaltyCardId.Value;
        if (valueObject is LoyaltyDiscountId loyaltyDiscountId)
            return loyaltyDiscountId.Value;
        if (valueObject is FranchiseId franchiseId)
            return franchiseId.Value;
        if (valueObject is GroupId groupId)
            return groupId.Value;
        if (valueObject is OrderItemId orderItemId)
            return orderItemId.Value;
        else
            throw new NotImplementedException();
    }

    public static int? GetId(this Payment? obj)
    {
        if (obj is not Payment payment)
            return null;

        return payment.Id.Value;
    }
}
