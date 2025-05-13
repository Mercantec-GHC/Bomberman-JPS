using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Bomberman_Backend.Repository.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using DomainModels;

namespace Bomberman_Backend.Repository;

public class TokenProvider : ITokenProvider
{
    private readonly IConfiguration _configuration;

    public TokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {

        string secretKey = _configuration["Jwt:Secret"] ?? Environment.GetEnvironmentVariable("secret");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"] ?? Environment.GetEnvironmentVariable("issuer"),
            Audience = _configuration["Jwt:Audience"] ?? Environment.GetEnvironmentVariable("audience"),
        };

        var handler = new JsonWebTokenHandler();
        
        string token = handler.CreateToken(tokenDescriptor);
        return token;
    }

    public string RefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}