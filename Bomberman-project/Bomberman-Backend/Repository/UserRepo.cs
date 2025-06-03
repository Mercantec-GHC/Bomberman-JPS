using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace Bomberman_Backend.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenProvider;

        public UserRepo(DatabaseContext databaseContext, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
        {
            _databaseContext = databaseContext;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
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

        public UpdateUserInfoDTO UpdateUser(Guid id, UpdateUserInfoDTO user)
        {
            var _user = _databaseContext.users.SingleOrDefault(o => o.UserId == id);

            if (_user == null)
            {
                return null;
            }

            _user.UserName = user.Username;
            _user.Email = user.Email;

            var User = _databaseContext.users.Update(_user);
            _databaseContext.SaveChanges();

            var userDTO = new UpdateUserInfoDTO
            {
                Username = User.Entity.UserName,
                Email = User.Entity.Email
            };
            return userDTO;
        }

        public UpdateUserPasswordDTO UpdateUserPassword(Guid id, UpdateUserPasswordDTO password)
        {
            var _user = _databaseContext.users.SingleOrDefault(o => o.UserId == id);

            if (_user == null)
            {
                return null;
            }

            _user.Password = _passwordHasher.Hash(password.Password);


            var userDTO = new UpdateUserPasswordDTO
            {
                Password = _user.Password,
            };

            var User = _databaseContext.users.Update(_user);
            _databaseContext.SaveChanges();
            return userDTO;
        }
    }
}
