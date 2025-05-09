using DomainModels.DTO;

namespace DomainModels.Mapper
{
    public static class LobbyMapper
    {
        public static Lobby toCreateLobby(this CreateLobbyDTO lobby)
        {
            return new Lobby
            {
                Name = lobby.Name,
                HostUserID = lobby.HostUserID
            };
        }
    }
}