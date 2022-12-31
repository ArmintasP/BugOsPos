﻿using ErrorOr;

namespace BugOsPos.Domain.Common.Errors;
public static partial class Errors
{
    public static class Group
    {
        public static Error NonExistentId => Error.NotFound(
            code: "Group.NonExistentId",
            description: "The provided group does not exist.");
    }
}
