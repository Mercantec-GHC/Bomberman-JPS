using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public void DeleteUser(Guid id)
        {
            var user = _databaseContext.users.Single(o => o.UserId == id);
            _databaseContext.users.Remove(user);
            _databaseContext.SaveChanges();
        }

        public GetUserDTO GetUser(Guid id)
        {
            var user = _databaseContext.users.SingleOrDefault(o => o.UserId == id);
            if (user == null)
            {
                return null;
            }
            var userDTO = new GetUserDTO
            {
                Id = user.Id,
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email
            };
            return userDTO;
        }

        public List<GetUserDTO> GetUsers()
        {
            var users = _databaseContext.users.ToList();
            List<GetUserDTO> getUserDTOs = new List<GetUserDTO>();
            foreach (User user in users)
            {
                var userDTO = new GetUserDTO
                {
                    Id = user.Id,
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email
                };

                getUserDTOs.Add(userDTO);
            }
            return getUserDTOs;
        }

        public GetUserDTO UpdateUser(Guid id, CreateUserDTO user)
        {
            var _user = _databaseContext.users.SingleOrDefault(o => o.UserId == id);

            if (_user == null)
            {
                return null;
            }

            _user.UserName = user.UserName;
            _user.Email = user.Email;
            _user.Password = user.Password;

            var User = _databaseContext.users.Update(_user);
            _databaseContext.SaveChanges();

            var userDTO = new GetUserDTO
            {
                Id = User.Entity.Id,
                UserId = User.Entity.UserId,
                UserName = User.Entity.UserName,
                Email = User.Entity.Email
            };
            return userDTO;
        }
    }
}
