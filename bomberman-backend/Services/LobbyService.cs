using bomberman_backend.Repository.Interfaces;
using bomberman_backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepo _lobbyRepo;

        public LobbyService(ILobbyRepo lobbyRepo)
        {
            _lobbyRepo = lobbyRepo;
        }

        public void AddUserToLobby(int lobbyId, Guid userId)
        {
            _lobbyRepo.AddUserToLobby(lobbyId, userId);
        }

        public Lobby CreateLobby(CreateLobbyDTO lobby)
        {
            return _lobbyRepo.CreateLobby(lobby);
        }

        public void DeleteLobby(int id)
        {
            _lobbyRepo.DeleteLobby(id);
        }

        public List<Lobby> GetLobbies()
        {
            return _lobbyRepo.GetLobbies();
        }

        public Lobby GetLobby(int id)
        {
            return _lobbyRepo.GetLobby(id);
        }

        public void RemoveUserFromLobby(int lobbyId, Guid userId)
        {
            _lobbyRepo.RemoveUserFromLobby(lobbyId, userId);
        }

        public Lobby UpdateLobby(int id, CreateLobbyDTO lobby)
        {
            return _lobbyRepo.UpdateLobby(id, lobby);
        }

        public List<Player> GetPlayers(int lobbyId)
        {
            return _lobbyRepo.GetPlayers(lobbyId);
        }
    }
}
