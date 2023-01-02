using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;

public static partial class Errors
{
	public static class Order
	{
		public static Error NotFound => Error.NotFound(
			code: "Order.NotFound",
			description: "Order with this id was not found.");
	}
}
