using DAL.Entites.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entites;

namespace DAL.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task Login(LoginRequestDTO loginRequestDTO);
    }
}
