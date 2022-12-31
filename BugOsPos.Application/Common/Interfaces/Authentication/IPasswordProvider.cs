namespace BugOsPos.Application.Common.Interfaces.Authentication;
public interface IPasswordProvider
{
    (string hashedPassword, byte[] salt) HashPassword(string password);
    bool VerifyPassword(string password, string hash, byte[] salt);
    string GenerateRandomPassword();
}
