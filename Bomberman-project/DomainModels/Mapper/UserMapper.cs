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