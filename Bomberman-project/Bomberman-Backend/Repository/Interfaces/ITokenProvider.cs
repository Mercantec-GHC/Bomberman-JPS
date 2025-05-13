using DomainModels;

namespace Bomberman_Backend.Repository.Interfaces;

public interface ITokenProvider
{
    public string GenerateToken(User user);
    
    public string RefreshToken();
}