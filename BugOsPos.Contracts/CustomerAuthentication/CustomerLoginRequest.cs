namespace BugOsPos.Contracts.CustomerAuthentication;

public record CustomerLoginRequest(
    int FranchiseId,
    string Username,
    string Password);
