using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;

public static partial class Errors
{
	public static class Order
	{
		public static Error NotFound => Error.NotFound(
			code: "Order.NotFound",
			description: "Order with this id was not found.");

		public static Error PaymentTypeIsNotValid => Error.NotFound(
			code: "Order.PaymentTypeIsNotValid",
			description: "Invalid payment type.");
	}
}
