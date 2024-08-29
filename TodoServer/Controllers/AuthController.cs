using AutoMapper;
using BLL.Model;
using BLL.Services.IService;
using BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TodoServer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _AuthService;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public AuthController(IAuthService AuthService, IMapper mapper)
        {
            _AuthService = AuthService;
            _mapper = mapper;
            this._response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] RegisterRequestDto model)
        {
            try
            {
                var user = await _AuthService.Register(model);
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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            try
            {
                var tokenDTO = await _AuthService.Login(model);
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
