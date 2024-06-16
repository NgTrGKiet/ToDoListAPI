using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTO
{
    public class RegisterRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
