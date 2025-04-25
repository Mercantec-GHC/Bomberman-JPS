using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Repository.Interfaces
{
    public interface IUserRepo
    {
        public GetUserDTO GetUser(Guid id);
        public List<GetUserDTO> GetUsers();

        public User CreateUser(CreateUserDTO user);
        public GetUserDTO UpdateUser(Guid id, CreateUserDTO user);
        public void DeleteUser(Guid id);
    }
}
