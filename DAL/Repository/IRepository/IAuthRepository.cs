using DAL.Entites.DTO;
using DAL.Entites;
using DAL.Entities.DTO;

namespace DAL.Repository.IRepository
{
    public interface IAuthRepository
    {
        public bool IsUniqueUser(string username);
        Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterRequestDTO registerationRequestDTO, User user);
    }
}
