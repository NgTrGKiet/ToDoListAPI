using DAL.Entites.DTO;
using DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entites;
using DAL.Entities.DTO;
using DAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DAL.Repository
{
    public class AuthRepository  : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        private readonly PasswordHasher<User> _passwordHasher;
        internal DbSet<User> _dbSet;
        public AuthRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _passwordHasher = new PasswordHasher<User>();
            this._dbSet = _db.Set<User>();
        }

        public async Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequestDTO.Password);
            if (result == PasswordVerificationResult.Failed || user == null)
            {
                throw new Exception("Something wrong");
            }

            var jwtTokenId = $"JTI{Guid.NewGuid()}";
            var accesstoken = await GetAccessToken(user, jwtTokenId);
            TokenDTO TokenDTO = new TokenDTO
            {
                AccessToken = accesstoken,
            };
            return TokenDTO;
        }
        public async Task<User> Register(RegisterRequestDTO registerationRequestDTO, User user)
        {
            await _dbSet.AddAsync(user);
            await _db.SaveChangesAsync();
            var userReturn = await _dbSet.FirstOrDefaultAsync(u => u.UserName == registerationRequestDTO.UserName);

            return userReturn;
        }

        private async Task<string> GetAccessToken(User user, string jwtTokenId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(token);
            return tokenStr;
        }

        public bool IsUniqueUser(string username)
        {
            //throw new NotImplementedException();
            var user = _dbSet.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }
    }
}
