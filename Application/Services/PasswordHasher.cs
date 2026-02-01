using Domain.Abstraction.Application;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasherService : IPasswordHasherService
{


    private const byte Version = 1;

    private const int SaltSize = 16; 
    private const int HashSize = 32; 
    private const int Iterations = 150_000;

    public byte[] Hash(string password)
    {
        ArgumentException.ThrowIfNullOrEmpty(password);

        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA512);

        var hash = pbkdf2.GetBytes(HashSize);

        var result = new byte[1 + SaltSize + HashSize];

        result[0] = Version;

        Buffer.BlockCopy(salt, 0, result, 1, SaltSize);
        Buffer.BlockCopy(hash, 0, result, 1 + SaltSize, HashSize);

        return result;
    }

    public bool Verify(string password, byte[] passwordHash)
    {
        if (passwordHash.Length < 1 + SaltSize + HashSize)
            return false;

        var version = passwordHash[0];

        if (version != Version)
            return false;

        var salt = new byte[SaltSize];
        var storedHash = new byte[HashSize];

        Buffer.BlockCopy(passwordHash, 1, salt, 0, SaltSize);
        Buffer.BlockCopy(passwordHash, 1 + SaltSize, storedHash, 0, HashSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA512);

        var computedHash = pbkdf2.GetBytes(HashSize);

        return CryptographicOperations.FixedTimeEquals(
            storedHash,
            computedHash);
    }
}
