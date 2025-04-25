using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DomainModels.DTO;

namespace DomainModels.Mapper
{
    public static class UsersMapper
    {
        public static User toCreateUser(this CreateUserDTO createUserDTO)
        {
            return new User
            {
                Email = createUserDTO.Email,
                Password = createUserDTO.Password,
                UserName = createUserDTO.UserName
            };
        }

        public static GetUserDTO toGetUser(this User user)
        {
            return new GetUserDTO
            {
                Id = user.Id,
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
