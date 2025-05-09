using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository.Interfaces
{
    public interface ILobbyRepo
    {
        public List<Lobby> GetLobbies();
        public Lobby GetLobby(int id);
        public Lobby CreateLobby(CreateLobbyDTO lobby);
        public Lobby UpdateLobby(int id, CreateLobbyDTO lobby);
        public void DeleteLobby(int id);
        public void AddUserToLobby(int lobbyId, Guid userId);
        public void RemoveUserFromLobby(int lobbyId, Guid userId);

        public List<Player> GetPlayers(int lobbyId);
    }
}
