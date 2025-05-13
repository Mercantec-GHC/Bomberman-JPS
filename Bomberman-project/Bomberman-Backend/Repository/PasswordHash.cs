using System.Security.Cryptography;
using Bomberman_Backend.Repository.Interfaces;

namespace Bomberman_Backend.Repository;

public class PasswordHash : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int HashIterations = 10000;

    private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA512;
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashIterations, algorithm, HashSize);
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split('-');
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);
        
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashIterations, algorithm, HashSize);
        return hash.SequenceEqual(inputHash);
    }
}