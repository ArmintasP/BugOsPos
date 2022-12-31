using BugOsPos.Domain.EmployeeAggregate;
using Microsoft.AspNetCore.Authorization;

namespace BugOsPos.Api.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params EmployeeRole[] roles) : base()
    {
        Roles = string.Join(',', roles);
    }
}
