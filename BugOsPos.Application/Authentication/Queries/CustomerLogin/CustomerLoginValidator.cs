using FluentValidation;

namespace BugOsPos.Application.Authentication.Queries.CustomerLogin;

public sealed class CustomerLoginValidator : AbstractValidator<CustomerLoginQuery>
{
    public CustomerLoginValidator()
    {
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
