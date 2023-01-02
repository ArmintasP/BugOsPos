using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class Group
    {
        public static Error NonExistentId => Error.NotFound(
            code: "Group.NonExistentId",
            description: "The provided group does not exist.");
        public static Error BadFranchiseId => Error.NotFound(
            code: "Group.BadFranchiseId",
            description: "The group is not in the same franchise.");
    }
}
