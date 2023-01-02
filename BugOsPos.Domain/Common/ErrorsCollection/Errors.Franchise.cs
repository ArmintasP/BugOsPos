using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class Franchise
    {
        public static Error NotFound => Error.NotFound(
            code: "Franchise.NotFound",
            description: "The franchise was not found.");
    }
}
