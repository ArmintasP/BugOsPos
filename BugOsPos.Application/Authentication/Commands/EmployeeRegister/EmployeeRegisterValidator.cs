using BugOsPos.Application.Common.Behaviors;
using BugOsPos.Application.Common.Interfaces.Clock;
using FluentValidation;

namespace BugOsPos.Application.Authentication.Commands.EmployeeRegister;

public sealed class EmployeeRegisterValidator : AbstractValidator<EmployeeRegisterCommand>
{
    private const int LegalWorkingAge = 18;
  
    public EmployeeRegisterValidator(IClock clock)
    {     
        RuleFor(x => x.FranchiseId).NotEmpty();
        RuleFor(x => x.Roles).EmployeeRoles();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.BankAccount).NotEmpty();
        RuleFor(x => x.DateOfBirth).GreaterThanOrEqualTo(
            DateOnly.FromDateTime(clock.UtcNow.AddYears(-LegalWorkingAge)));
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Employment)
            .LessThanOrEqualTo(1)
            .GreaterThan(0);
    }
}
