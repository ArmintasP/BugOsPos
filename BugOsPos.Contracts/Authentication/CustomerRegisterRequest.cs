namespace BugOsPos.Contracts.Authentication;

public record CustomerRegisterRequest(
    int FranchiseId,
    string Username,
    string Password,
    string Email,
    string Name,
    string Surname);
