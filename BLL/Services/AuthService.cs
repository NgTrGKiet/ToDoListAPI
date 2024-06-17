using BLL.Services.IService;
using DAL.Entites.DTO;
using DAL.Entites;
using DAL.Entities.DTO;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _dbAuth;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IAuthRepository dbAuth)
        {
            _dbAuth = dbAuth;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            bool checkUserName = _dbAuth.IsUniqueUser(loginRequestDTO.UserName);
            if(checkUserName)
            {
                throw new Exception("UserName doesn't exist");
            }
           if(loginRequestDTO.Password.Length < 8)
            {
                throw new Exception("Password must have at least 8 characters");
            }
            var TokenReturn = await _dbAuth.Login(loginRequestDTO);
            if (TokenReturn == null)
            {
                throw new Exception("Username or password is incorrect");
            }
            return TokenReturn;
            
        }
        public async Task<User> Register(RegisterRequestDTO registerationRequestDTO)
        {
            bool ifUserNameUnique = _dbAuth.IsUniqueUser(registerationRequestDTO.UserName);
            if (ifUserNameUnique == false)
            {
                throw new Exception("Username already exists");
            }
            if (registerationRequestDTO.Password.Length < 8)
            {
                throw new Exception("Password must have at least 8 characters");
            }
            User user = new()
            {
                UserName = registerationRequestDTO.UserName,
                Name = registerationRequestDTO.Name,
                Id = Guid.NewGuid().ToString("N").Substring(0, 5),
            };
            user.Password = _passwordHasher.HashPassword(user, registerationRequestDTO.Password);
            var UserReturn = await _dbAuth.Register(registerationRequestDTO, user);
            
            if(UserReturn == null)
            {
                throw new Exception("Error while registering");
            }
            return UserReturn;
        }
    }
}
