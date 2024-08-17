using System.Security.Cryptography;

namespace Web.Api.Users;

public sealed class PasswordHasher
{
    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 32;
    private const int ITERATIONS = 100000;

    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;
    
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, _algorithm, HASH_SIZE);
        
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        string[] passwordHashParts = passwordHash.Split('-');
        byte[] salt = Convert.FromHexString(passwordHashParts[1]);
        byte[] hash = Convert.FromHexString(passwordHashParts[0]);
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, _algorithm, HASH_SIZE);
        
        // vulnerable to timing attack
        // return hash.SequenceEqual(inputHash);
        
        return CryptographicOperations .FixedTimeEquals(hash, inputHash);
    }
}