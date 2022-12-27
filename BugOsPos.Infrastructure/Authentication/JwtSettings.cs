﻿namespace BugOsPos.Infrastructure.Authentication;

public class JwtSettings
{
    public string Secret { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiryInMinutes { get; set; }
}