using bomberman_backend.Repository.Interfaces;
using bomberman_backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public User CreateUser(CreateUserDTO user)
        {
           return _userRepo.CreateUser(user);
        }

        public void DeleteUser(Guid id)
        {
            _userRepo.DeleteUser(id);
        }

        public GetUserDTO GetUser(Guid id)
        {
            return _userRepo.GetUser(id);
        }

        public List<GetUserDTO> GetUsers()
        {
            return _userRepo.GetUsers();
        }

        public GetUserDTO UpdateUser(Guid id, CreateUserDTO user)
        {
            return _userRepo.UpdateUser(id, user);
        }
    }
}
