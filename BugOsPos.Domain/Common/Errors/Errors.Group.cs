using ErrorOr;

namespace BugOsPos.Domain.Common.Errors;
public static partial class Errors
{
    public static class Customer
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "Customer.DuplicateEmail",
            description: "The email is already in use by another customer.");

        public static Error DuplicateUsername => Error.Conflict(
            code: "Customer.DuplicateUsername",
            description: "The username is already in use by another customer.");
    }
}
