namespace BugOsPos.Contracts.Common;

public sealed record EmployeeSection(
    int Id,
    string EmployeeCode,
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