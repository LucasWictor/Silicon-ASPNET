using System.Security.Cryptography;
using System.Text;
using Infrastructure.Services;

namespace Infrastructure.Helpers;

public class PasswordHasher
{
    public static (string Hash, string SecurityKey) GenerateSecurePassword(string password)
    {
        using var hmac = new HMACSHA512();
        var securityKey = Convert.ToBase64String(hmac.Key);
        var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

        Console.WriteLine($"Generated Hash: {hash}");
        Console.WriteLine($"Generated Security Key: {securityKey}");

        return (hash, securityKey);
    }

    public static bool ValidateSecurePassword(string password, string storedHash, string securityKey)
    {
        var securityKeyBytes = Convert.FromBase64String(securityKey);
        using var hmac = new HMACSHA512(securityKeyBytes);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computedHashBase64 = Convert.ToBase64String(computedHash);

        Console.WriteLine($"Computed Hash: {computedHashBase64}");
        Console.WriteLine($"Stored Hash: {storedHash}");

        return computedHashBase64 == storedHash;
    }
}
