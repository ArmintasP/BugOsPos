using FluentValidation;

namespace BugOsPos.Application.Authentication.Queries.EmployeeLogin;

public sealed class EmployeeLoginQueryValidator : AbstractValidator<EmployeeLoginQuery>
{
    public EmployeeLoginQueryValidator()
    {
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.EmployeeCode).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
