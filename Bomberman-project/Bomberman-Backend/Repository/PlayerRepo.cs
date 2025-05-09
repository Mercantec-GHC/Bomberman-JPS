using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository
{
    public class PlayerRepo : IPlayerRepo
    {
        private readonly DatabaseContext _databaseContext;
        public PlayerRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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
                Password = player.password,
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