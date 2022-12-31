namespace BugOsPos.Contracts.CustomerAuthentication;
public record CustomerAuthenticationResponse(
    int Id,
    int FranchiseId,
    string Username,
    string Name,
    string Surname,
    string Token);
