namespace BugOsPos.Contracts.Employees;

public sealed record EmployeeUpdateRequest(
    string Email,
    string NewPassword,
    string Name,
    string Surname,
    string BankAccount,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    int ReadAccess,
    decimal Employment,
    List<ShiftSectionRequest> Shifts);

public sealed record ShiftSectionRequest(
    int LocationId,
    DateTime Start,
    DateTime End);
