namespace BugOsPos.Contracts.Employees;

public sealed record EmployeeUpdateResponse(
    string Email,
    string Name,
    string Surname,
    string BankAccount,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    int ReadAccess,
    List<string> Roles,
    decimal Employment,
    List<ShiftSectionResponse> Shifts);

public sealed record ShiftSectionResponse(
    int Id,
    int LocationId,
    DateTime Start,
    DateTime End);