using BugOsPos.Application.Common.Behaviors;
using FluentValidation;

namespace BugOsPos.Application.Authentication.Commands.Register;

public class CustomerRegisterValidator : AbstractValidator<CustomerRegisterCommand>
{
    private const int MinUsernameLength = 4;
    private const int MaxUsernameLength = 12;
    private const int MinPasswordLength = 8;
    private const int MaxPasswordLength = 32;

    public CustomerRegisterValidator()
    {
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Username)
            .MinimumLength(MinUsernameLength)
            .MaximumLength(MaxUsernameLength);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).Password(MinPasswordLength, MaxPasswordLength);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
    }
}
