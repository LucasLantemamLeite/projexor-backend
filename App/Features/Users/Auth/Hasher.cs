using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace App.Features.Users.Auth;

public static class Hasher
{
    private static readonly int Memory = 1024 * 128;
    private static readonly int Parallelism = 2;
    private static readonly int Iterations = 4;

    public static string GenerateHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);

        var argon = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            MemorySize = Memory,
            DegreeOfParallelism = Parallelism,
            Iterations = Iterations
        };

        var hash = argon.GetBytes(32);

        return Convert.ToBase64String(salt.Concat(hash).ToArray());
    }

    public static bool VerifyHash(string storagePassword, string confirmPassword)
    {
        var decode = Convert.FromBase64String(storagePassword);

        var salt = decode.Take(16).ToArray();

        var storageHash = decode.Skip(16).ToArray();

        var argon = new Argon2id(Encoding.UTF8.GetBytes(confirmPassword))
        {
            Salt = salt,
            MemorySize = Memory,
            DegreeOfParallelism = Parallelism,
            Iterations = Iterations
        };

        var hash = argon.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(storageHash, hash);
    }
}