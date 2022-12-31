using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: "Authentication.InvalidCredentials",
            description: "Invalid username or password.");

        public static Error FranchiseIdMissing => Error.Unexpected(
            code: "Authentication.FranchiseIdMissing",
            description: "Franchise id associated with login session is missing.");

        public static Error InvalidFranchiseId => Error.Validation(
            code: "Authentication.InvalidFranchiseId",
            description: "Franchise id associated with login session is corrupted.");
    }
}
