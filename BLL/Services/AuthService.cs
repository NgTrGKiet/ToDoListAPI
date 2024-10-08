﻿using BLL.Services.IService;
using DAL.Entities;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using BLL.DTO;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<TokenDTO> Login(LoginRequestDto loginRequestDTO)
        {
            bool checkUserName = _authRepo.IsUniqueUser(loginRequestDTO.UserName);
            if(checkUserName)
            {
                throw new Exception("UserName doesn't exist");
            }
           if(loginRequestDTO.Password.Length < 8)
            {
                throw new Exception("Password must have at least 8 characters");
            }
            var TokenReturn = await _authRepo.Login(loginRequestDTO.UserName, loginRequestDTO.Password); 
            if (TokenReturn == null)
            {
                throw new Exception("Username or password is incorrect");
            }
            TokenDTO TokenDTO = new TokenDTO
            {
                AccessToken = TokenReturn,
            };
            return TokenDTO;
            
        }
        public async Task<User> Register(RegisterRequestDto registerationRequestDto)
        {
            bool ifUserNameUnique = _authRepo.IsUniqueUser(registerationRequestDto.UserName);
            if (ifUserNameUnique == false)
            {
                throw new Exception("Username already exists");
            }
            if (registerationRequestDto.Password.Length < 8)
            {
                throw new Exception("Password must have at least 8 characters");
            }
            User user = new()
            {
                UserName = registerationRequestDto.UserName,
                Name = registerationRequestDto.Name,
                Id = Guid.NewGuid().ToString("N").Substring(0, 5),
            };
            user.Password = _passwordHasher.HashPassword(user, registerationRequestDto.Password);
            var UserReturn = await _authRepo.Register(user);
            
            if(UserReturn == null)
            {
                throw new Exception("Error while registering");
            }
            return UserReturn;
        }
    }
}
