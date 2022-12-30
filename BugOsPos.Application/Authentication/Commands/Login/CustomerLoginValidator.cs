using FluentValidation;

namespace BugOsPos.Application.Authentication.Commands.Login;

public sealed class CustomerLoginValidator : AbstractValidator<CustomerLoginCommand>
{
    public CustomerLoginValidator()
    {
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
