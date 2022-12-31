using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Clock;
using BugOsPos.Domain.CustomerAggregate;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugOsPos.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IClock _clock;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IClock clock, IOptions<JwtSettings> jwtSettings)
    {
        _clock = clock;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(Customer customer)
    {
        var claims = GetMainClaims(
            customer.FranchiseId,
            customer.Id.ToString(),
            customer.Username,
            customer.Name,
            customer.Surname);

        var token = GenerateToken(claims);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateToken(Employee employee)
    {
        var claims = GetMainClaims(
            employee.FranchiseId,
            employee.Id.ToString(),
            employee.EmployeeCode,
            employee.Name,
            employee.Surname);

        foreach (var type in employee.Roles)
            claims.Add(new Claim(ClaimTypes.Role, type.ToString()));

        var token = GenerateToken(claims);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GenerateToken(List<Claim> claims)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _clock.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            claims: claims,
            signingCredentials: signingCredentials);
        return token;
    }

    private static List<Claim> GetMainClaims(FranchiseId franchiseId, string entityId, string username, string name, string surname)
    {
        return new()
        {
            new Claim(JwtSettings.FranchiseClaim, franchiseId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, entityId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Name, name),
            new Claim(JwtRegisteredClaimNames.FamilyName, surname),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
    }
}
