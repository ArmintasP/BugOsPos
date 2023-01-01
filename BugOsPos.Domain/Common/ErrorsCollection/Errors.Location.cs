using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class Location
    {
        public static Error NotFound => Error.NotFound(
            code: "Location.NotFound",
            description: "The location was not found.");
    }
}
