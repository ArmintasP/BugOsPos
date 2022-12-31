using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class Employee
    {
        public static Error NotFound => Error.NotFound(
            code: "Employee.NotFound",
            description: "The employee was not found.");

        public static Error Forbidden => Error.Custom(
            type: (int)CustomErrorType.Forbidden,
            code: "Employee.Forbidden",
            description: "The employee is not allowed to perform this action.");
    }
}
