using bomberman_backend.Data;
using bomberman_backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Repository
{
    public class LobbyRepo : ILobbyRepo
    {
        private readonly DatabaseContextcs _databaseContext;
        public LobbyRepo(DatabaseContextcs databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public void AddUserToLobby(int lobbyId, Guid userId)
        {
            List<Player> players = new List<Player>();
            var lobby = _databaseContext.lobby.SingleOrDefault(o => o.Id == lobbyId);
            var player = _databaseContext.players.SingleOrDefault(o => o.UserId == userId);


            if (lobby == null || player == null)
            {
                throw new Exception("Lobby or Player not found");
            }
            players.Add(player);
            lobby.Players = players;
            player.lobby = lobby;
            _databaseContext.players.Update(player);
            _databaseContext.lobby.Update(lobby);
            _databaseContext.SaveChanges();


        }

        public Lobby CreateLobby(CreateLobbyDTO lobby)
        {
            var players = new List<Player>();
            var hostPlayer = _databaseContext.players.SingleOrDefault(o => o.UserId == lobby.HostUserID);
            players.Add(hostPlayer);
            var newLobby = new Lobby()
            {
                Name = lobby.Name,
                HostUserID = lobby.HostUserID,
                Players = players
            };
            _databaseContext.lobby.Add(newLobby);
            _databaseContext.SaveChanges();
            return newLobby;
        }

        public void DeleteLobby(int id)
        {
            var lobby = _databaseContext.lobby.Find(id);

            if (lobby == null)
            {
                throw new Exception("Lobby not found");
            }
            _databaseContext.lobby.Remove(lobby);
            _databaseContext.SaveChanges();
        }

        public List<Lobby> GetLobbies()
        {
            var lobbies = _databaseContext.lobby.ToList();
            foreach(var lobby in lobbies)
            {
                var lobbyId = lobby.Id;
                var lobbyPlayers = GetPlayers(lobbyId);
                lobby.Players = lobbyPlayers;
            }
            return lobbies;
        }

        public Lobby GetLobby(int id)
        {
            var lobby = _databaseContext.lobby.Find(id);
            if(lobby == null)
            {
                throw new Exception("Lobby not found");
            }
            lobby.Players = GetPlayers(id);
            return lobby;
        }

        public void RemoveUserFromLobby(int lobbyId, Guid userId)
        {
            var lobby = _databaseContext.lobby.Find(lobbyId);
            var player = _databaseContext.players.SingleOrDefault(o => o.UserId == userId);
            if (lobby == null || player == null)
            {
                throw new Exception("Lobby or Player not found");
            }

            lobby.Players.Remove(player);
            player.lobby = null;
            _databaseContext.players.Update(player);
            _databaseContext.lobby.Update(lobby);
            _databaseContext.SaveChanges();
        }

        public Lobby UpdateLobby(int id, CreateLobbyDTO lobby)
        {
            var getLobby = _databaseContext.lobby.Find(id);

            if (getLobby == null)
            {
                throw new Exception("Lobby not found");
            }
            getLobby.Name = lobby.Name;
            getLobby.HostUserID = lobby.HostUserID;
            _databaseContext.lobby.Update(getLobby);
            _databaseContext.SaveChanges();
            return getLobby;
        }

        public List<Player> GetPlayers(int lobbyId)
        {
            var players = _databaseContext.players.Where(o => o.lobby.Id == lobbyId).ToList();
            if (players == null)
            {
                throw new Exception("Lobby not found");
            }
            return players;
        }
    }
}
