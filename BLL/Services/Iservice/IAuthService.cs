using DAL.Entites.DTO;
using DAL.Entites;
using DAL.Entities.DTO;

namespace BLL.Services.IService
{
    public interface IAuthService
    {
        Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterRequestDTO registerationRequestDTO);
    }
}
