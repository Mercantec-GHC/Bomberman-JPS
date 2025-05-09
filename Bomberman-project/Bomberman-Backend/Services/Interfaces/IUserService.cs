using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Services.Interfaces
{
    public interface IUserService
    {
        public GetUserDTO GetUser(Guid id);
        public List<GetUserDTO> GetUsers();
        public GetUserDTO UpdateUser(Guid id, CreateUserDTO user);
        public void DeleteUser(Guid id);
    }
}