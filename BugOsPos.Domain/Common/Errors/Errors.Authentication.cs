using ErrorOr;

namespace BugOsPos.Domain.Common.Errors;
public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: "Authentication.InvalidCredentials",
            description: "Invalid username or password.");
    }
}
