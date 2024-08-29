using BLL.DTO;
using BLL.Model;
using DAL.Entities;

namespace BLL.Services.IService
{
    public interface IAuthService
    {
        Task<TokenDTO> Login(LoginRequestDto loginRequestDto);
        Task<User> Register(RegisterRequestDto registerationRequestDTO);
    }
}
