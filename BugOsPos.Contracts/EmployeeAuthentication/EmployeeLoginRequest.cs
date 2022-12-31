namespace BugOsPos.Contracts.EmployeeAuthentication;

public sealed record EmployeeLoginRequest(
    int FranchiseId,
    string EmployeeCode,
    string Password);
