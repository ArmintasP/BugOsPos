namespace BugOsPos.Application.Common.Interfaces.Authentication;
public interface IPasswordHasher
{
    (string hashedPassword, byte[] salt) Hash(string password);
    bool Verify(string password, string hash, byte[] salt);
}
