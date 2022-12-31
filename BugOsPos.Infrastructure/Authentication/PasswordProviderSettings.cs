using System.Security.Cryptography;

namespace BugOsPos.Infrastructure.Authentication;

public sealed record PasswordProviderSettings
{
    private readonly HashAlgorithmName _algorithm;
    private readonly string _algorithmName = null!;
    
    public int KeySize { get; init; }
    public int Iterations { get; init; }
    public HashAlgorithmName Algorithm => _algorithm;
    public string AlgorithmName
    {
        get => _algorithmName;
        init
        {
            _algorithmName = value;
            _algorithm = new HashAlgorithmName(_algorithmName);
        }
    }
}
