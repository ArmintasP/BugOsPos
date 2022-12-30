namespace BugOsPos.Infrastructure.Authentication;

public sealed record JwtSettings
{
    public string Secret { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public int ExpiryInMinutes { get; init; }
}
