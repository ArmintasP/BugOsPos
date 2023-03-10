using BugOsPos.Contracts.Common;

namespace BugOsPos.Contracts.EmployeeAuthentication;

public sealed record EmployeeRegisterResponse(
    int Id,
    string EmployeeCode,
    string Password,
    int FranchiseId,
    string Email,
    string Name,
    string Surname,
    string BankAccount,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    decimal Rating,
    int ReadAccess,
    List<string> Roles,
    decimal Employment,
    List<ShiftSection>? Shifts);
