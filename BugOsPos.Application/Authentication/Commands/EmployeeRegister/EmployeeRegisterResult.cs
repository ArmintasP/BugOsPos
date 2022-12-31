using BugOsPos.Domain.EmployeeAggregate;

namespace BugOsPos.Application.Authentication.Commands.EmployeeRegister;
public record EmployeeRegisterResult(
    Employee Employee,
    string Password);
