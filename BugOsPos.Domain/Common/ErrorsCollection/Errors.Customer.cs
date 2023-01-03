using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
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

        public static Error NotFound => Error.NotFound(
            code: "Customer.NotFound",
            description: "The customer was not found.");
        
        public static Error Unauthorized => Error.Conflict(
            code: "Customer.Unauthorized",
            description: "Unauthorized access.");
    }
}
