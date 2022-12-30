using BugOsPos.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace BugOsPos.Infrastructure.Authentication;

public sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasherSettings _settings;

    public PasswordHasher(IOptions<PasswordHasherSettings> settings)
    {
        _settings = settings.Value;
    }

    public (string hashedPassword, byte[] salt) Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_settings.KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            _settings.Iterations,
            _settings.Algorithm,
            _settings.KeySize);

        return (Convert.ToHexString(hash), salt);
    }

    public bool Verify(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            _settings.Iterations,
             _settings.Algorithm,
            _settings.KeySize);

        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }
}
