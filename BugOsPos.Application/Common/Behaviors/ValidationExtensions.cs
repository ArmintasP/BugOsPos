using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using FluentValidation;

namespace BugOsPos.Application.Common.Behaviors;
public static class ValidationExtensions
{
    public static IRuleBuilderOptionsConditions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minLength = 8, int maxLength = 32)
    {
        return ruleBuilder.Custom((password, context) =>
        {
            if (string.IsNullOrWhiteSpace(password))
                context.AddFailure("Password cannot be empty");
            if (password.Length < minLength)
                context.AddFailure($"Password must be at least {minLength} characters long");
            if (password.Length > maxLength)
                context.AddFailure($"Password must be at most {maxLength} characters long");
            if (!password.Any(char.IsUpper))
                context.AddFailure("Password must contain at least one uppercase letter");
            if (!password.Any(char.IsLower))
                context.AddFailure("Password must contain at least one lowercase letter");
            if (!password.Any(char.IsDigit))
                context.AddFailure("Password must contain at least one digit");
        });
    }

    public static IRuleBuilderOptionsConditions<T, List<string>> EmployeeRoles<T>(
        this IRuleBuilder<T, List<string>> ruleBuilder)
    {
        return ruleBuilder.Custom((roles, context) =>
        {
            var existingRoles = Enum.GetNames(typeof(EmployeeRole)).ToHashSet();
            
            foreach(var role in roles)
            {
                if (!existingRoles.Contains(role))
                    context.AddFailure($"Role {role} does not exist");
            }
        });
    }

    public static IRuleBuilderOptionsConditions<T, string> PaymentType<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((type, context) =>
        {
            var existingRoles = Enum.GetNames(typeof(PaymentType)).ToHashSet();

            if (!existingRoles.Contains(type))
                context.AddFailure($"Role {type} does not exist");
        });
    }
}
