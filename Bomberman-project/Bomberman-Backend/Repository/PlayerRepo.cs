using System.Security.Claims;
using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.EntityFrameworkCore;

namespace Bomberman_Backend.Repository
{
    public class PlayerRepo : IPlayerRepo
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PlayerRepo(DatabaseContext databaseContext, IPasswordHasher passwordHasher, ITokenProvider tokenProvider, IHttpContextAccessor httpContextAccessor)
        {
            _databaseContext = databaseContext;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
            _httpContextAccessor = httpContextAccessor;
        }
        public Player AddBomb(string username, Bomb bomb)
        {
            var player = _databaseContext.players.SingleOrDefault(o => o.UserName == username);
            if (player == null)
            {
                return null;
            }
            var bombToAdd = _databaseContext.bomb.SingleOrDefault(o => o.Id == bomb.Id);

            if (bombToAdd == null)
            {
                bombToAdd = new Bomb
                {
                    Id = bomb.Id,
                    xCordinate = bomb.xCordinate,
                    yCordinate = bomb.yCordinate,
                    explosionRadius = bomb.explosionRadius,
                    fuseTime = bomb.fuseTime
                };
                _databaseContext.bomb.Add(bombToAdd);
            }

            player.bomb = bombToAdd;
            _databaseContext.players.Update(player);
            _databaseContext.SaveChanges();
            return player;

        }

        public Player AddPowerUp(string username, PowerUp powerup)
        {
            var player = _databaseContext.players.SingleOrDefault(o => o.UserName == username);
            if (player == null)
            {
                return null;
            }
            var powerUpToAdd = _databaseContext.powerup.SingleOrDefault(o => o.Id == powerup.Id);
            if (powerUpToAdd == null)
            {
                powerUpToAdd = new PowerUp
                {
                    Id = powerup.Id,
                    Name = powerup.Name,
                    Effect = powerup.Effect,
                    duration = powerup.duration
                };
                _databaseContext.powerup.Add(powerUpToAdd);
            }
            player.powerUp = powerUpToAdd;
            _databaseContext.players.Update(player);
            _databaseContext.SaveChanges();
            return player;
        }
        
        

        public TokenResponse Login(LoginDTO login)
        {
            User? user = _databaseContext.users.SingleOrDefault(o => o.UserName == login.username);
            bool verified = _passwordHasher.Verify(login.password, user.Password);
            if (!verified)
            {
                throw new ApplicationException("Invalid username or password");
            }

            string token = _tokenProvider.GenerateToken(user);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.UserId,
                Token = _tokenProvider.RefreshToken(),
                Expires = DateTime.UtcNow.AddDays(1)
            };
            
            
            _databaseContext.refreshTokens.Add(refreshToken);
            _databaseContext.SaveChanges();
            return new TokenResponse(token, refreshToken.Token);
        }

        public TokenResponse LoginWithRefreshToken(TokenRequest tokenRequest)
        {
            RefreshToken refreshToken = _databaseContext.refreshTokens.Include(r => r.User).FirstOrDefault(o => o.Token == tokenRequest.refreshtoken);

            if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
            {
                throw new ApplicationException("Invalid refresh token");
            }
            
            string accesToken = _tokenProvider.GenerateToken(refreshToken.User);
            refreshToken.Token = _tokenProvider.RefreshToken();
            refreshToken.Expires = DateTime.UtcNow.AddDays(1);

            _databaseContext.SaveChanges();
            return new TokenResponse(accesToken, refreshToken.Token);
        }

        public bool RevokeRefreshToken(Guid userId)
        {
            if (userId != GetCurrentUser())
            {
                throw new ApplicationException("Invalid userId");
            }

            _databaseContext.refreshTokens.Where(r => r.UserId == userId).ExecuteDelete();
            return true;
        }

        private Guid? GetCurrentUser()
        {
            return Guid.TryParse(
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), 
                    out Guid parsed) 
                    ? parsed : null;
        }
        public Player CreatePlayer(CreatePlayerDTO player)
        {
            if (object.ReferenceEquals(player, null))
            {
                return null;
            }
            List<string> colors = new List<string>()
            {
                "Red",
                "Blue",
                "Green",
                "Orange",
            };

            Random rnd = new Random();
            var rndColor = rnd.Next(0, 4);

            var newPlayer = new Player
            {
                UserId = Guid.NewGuid(),
                UserName = player.userName,
                Email = player.email,
                Password = _passwordHasher.Hash(player.password),
                score = 0,
                lives = 3,
                characterColor = colors[rndColor],
                wins = 0,
                sessionId = player.sessionId,
                bomb = new Bomb
                {
                    xCordinate = "0",
                    yCordinate = "0",
                    explosionRadius = 0,
                    fuseTime = 0
                },
            };
            _databaseContext.players.Add(newPlayer);
            _databaseContext.SaveChanges();
            return newPlayer;
        }

        public Player GetPlayer(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            var player = _databaseContext.players.SingleOrDefault(o => o.UserId == id);

            if (player == null)
            {
                return null;
            }
            return player;
        }

        public List<Player> GetPlayers()
        {
            return _databaseContext.players.ToList();
        }

        public Player RemoveBomb(string username, Bomb bomb)
        {
            var player = _databaseContext.players.SingleOrDefault(o => o.UserName == username);
            if (player == null)
            {
                return null;
            }
            var bombToRemove = _databaseContext.bomb.SingleOrDefault(o => o.Id == bomb.Id);
            if (bombToRemove == null)
            {
                return null;
            }
            else
            {

                _databaseContext.bomb.Remove(bombToRemove);
                bombToRemove = null;
            }

            player.bomb = bombToRemove;
            _databaseContext.SaveChanges();
            return player;
        }

        public Player RemovePowerUp(string username, PowerUp powerup)
        {
            var player = _databaseContext.players.SingleOrDefault(o => o.UserName == username);
            if (player == null)
            {
                return null;
            }
            var powerUpToRemove = _databaseContext.powerup.SingleOrDefault(o => o.Id == powerup.Id);
            if (powerUpToRemove == null)
            {
                return null;
            }
            else
            {
                _databaseContext.powerup.Remove(powerUpToRemove);
                powerUpToRemove = null;
            }
            player.powerUp = powerUpToRemove;
            _databaseContext.SaveChanges();
            return player;
        }

        public Player UpdatePlayer(UpdatePlayerDTO player)
        {
            if (string.IsNullOrEmpty(player.Username))
            {
                return null;
            }
            var updatePlayer = _databaseContext.players.SingleOrDefault(o => o.UserName == player.Username);
            if (updatePlayer == null)
            {
                return null;
            }
            updatePlayer.score = player.Score;
            updatePlayer.lives = player.Lives;
            updatePlayer.characterColor = player.CharacterColor;
            updatePlayer.wins = player.Wins;
            updatePlayer.inLobby = player.InLobby;
            updatePlayer.powerUp = player.PowerUp;
            updatePlayer.sessionId = player.sessionId;
            _databaseContext.SaveChanges();
            return updatePlayer;
        }
    }
}