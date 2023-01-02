using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class LoyaltyCard
    {
        public static Error NotFound => Error.NotFound(
            code: "LoyaltyCard.NotFound",
            description: "The loyalty card was not found.");
    }
}
