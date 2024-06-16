using DAL.Entites.DTO;
using DAL.Entites;
using DAL.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IService
{
    public interface IAuthService
    {
        Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterRequestDTO registerationRequestDTO);
    }
}
