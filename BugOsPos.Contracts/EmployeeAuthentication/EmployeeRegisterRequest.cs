namespace BugOsPos.Contracts.EmployeeAuthentication;

public sealed record EmployeeRegisterRequest(
    int? GroupId,
    string Email,
    string Name,
    string Surname,
    string BankAccount,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    int ReadAccess,
    List<string> Roles,
    decimal Employment);
