﻿using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Services.Interfaces;
using DomainModels.DTO;

namespace Bomberman_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
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
