namespace BugOsPos.Contracts.Authentication;

public record CustomerLoginRequest(
    int FranchiseId,
    string Username,
    string Password);
