using System.Security.Cryptography;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace MinecraftServer;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);

        var config = new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 3,
            MemoryCost = 262144,
            Lanes = 4,
            Threads = 4,
            Password = System.Text.Encoding.UTF8.GetBytes(password),
            Salt = salt,
            HashLength = 32
        };

        using var argon2 = new Argon2(config);
        using var hash = argon2.Hash();

        return config.EncodeString(hash.Buffer);
    }

    public static bool VerifyPassword(string password, string encodedHash)
    {
        return Argon2.Verify(encodedHash, password);
    }
}