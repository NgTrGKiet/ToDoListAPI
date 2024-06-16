using AutoMapper;
using BLL.Model;
using BLL.Services.IService;
using DAL.Entites.DTO;
using DAL.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace TodoServer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _dbauth;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public AuthController(IAuthService dbauth, IMapper mapper)
        {
            _dbauth = dbauth;
            _mapper = mapper;
            this._response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] RegisterRequestDTO model)
        {
            try
            {
                var user = await _dbauth.Register(model);
                _response.Result = _mapper.Map<UserDTO>(user);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            try
            {
                var tokenDTO = await _dbauth.Login(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = tokenDTO;
                return Ok(_response);
            } catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

    }
}
