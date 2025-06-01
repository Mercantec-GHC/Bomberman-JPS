using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository.Interfaces
{
    public interface IUserRepo
    {
        public GetUserDTO GetUser(Guid id);
        public List<GetUserDTO> GetUsers();
        public UpdateUserInfoDTO UpdateUser(Guid id, UpdateUserInfoDTO user);
        public UpdateUserPasswordDTO UpdateUserPassword(Guid id, string password);
        public void DeleteUser(Guid id);
    }
}