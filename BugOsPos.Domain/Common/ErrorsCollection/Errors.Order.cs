using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;

public static partial class Errors
{
	public static class Order
	{
		public static Error NotFound => Error.NotFound(
			code: "Order.NotFound",
			description: "Order with this id was not found.");
		
		public static Error AlreadyConfirmed => Error.NotFound(
			code: "Order.AlreadyConfirmed",
			description: "Order is already placed and have been confirmed in the past.");		
		
		public static Error AlreadyCompleted => Error.NotFound(
			code: "Order.AlreadyCompleted",
			description: "Order has been already completed.");
		
		public static Error OrderItemNotFound => Error.NotFound(
			code: "Order.OrderItemNotFound",
			description: "Orderitem with this id was not found.");

		public static Error PaymentTypeIsNotValid => Error.NotFound(
			code: "Order.PaymentTypeIsNotValid",
			description: "Invalid payment type.");

		public static Error PaymentAlreadyInitiated => Error.Conflict(
            code: "Order.PaymentAlreadyInitiated",
            description: "Payment has already been initiated.");

		public static Error PaymentIsMissing => Error.Conflict(
			code: "Order.PaymentIsMissing",
			description: "Order did not receive payment yet");
        public static Error ProductNoLongerExists => Error.Conflict(
            code: "Order.ProductNoLongerExists",
            description: "Ordered product no longer exists, unable to perform actions on it.");

        public static Error OrderItemAlreadyRated => Error.Conflict(
            code: "Order.OrderItemAlreadyRated",
            description: "Order item has already been rated.");
    }
}
