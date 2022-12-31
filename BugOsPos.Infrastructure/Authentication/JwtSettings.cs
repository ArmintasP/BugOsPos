namespace BugOsPos.Infrastructure.Authentication;

public sealed record JwtSettings
{
    public const string FranchiseClaim = "Franchise";
    public const string EployeeClaim = "Employee";

    public string Secret { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public int ExpiryInMinutes { get; init; }
}
