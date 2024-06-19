using AutoMapper;
using BLL.Model;
using BLL.Services.IService;
using DAL.Entities;
using BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace TodoServer.Controllers
{
    [ApiController]
    [Route("api/usertask")]
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _userTaskService;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public UserTaskController(IUserTaskService UserTaskService, IMapper mapper)
        {
            _userTaskService = UserTaskService;
            this._response = new();
            _mapper = mapper;
        }

        [HttpGet("all-task")]
        //[ResponseCache(Duration = 30)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetTasks([FromQuery] FilterModel filter)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                IEnumerable<UserTask> taskLists = await _userTaskService.GetAllTasksService(userId, filter);
                _response.Result = _mapper.Map<List<TaskDTO>>(taskLists);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
                return BadRequest(_response);
            }
        }

        [HttpGet("single-task/{id:int}", Name = "GetTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var task = await _userTaskService.GetTaskService(id, userId);

                _response.Result = _mapper.Map<TaskDTO>(task);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            } catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDTO createDTO)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                UserTask task = _mapper.Map<UserTask>(createDTO);
                task.User_id = userId;

                await _userTaskService.CreateTaskService(task, userId);


                _response.Result = _mapper.Map<TaskDTO>(task);
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

        [HttpPut("{id:int}", Name = "UpdateTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDTO updateDTO)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("Unauthorized");
                }
                if (updateDTO == null || id != updateDTO.Id) 
                {
                    return BadRequest();
                }

                if(await _userTaskService.GetTaskService(id, userId) ==  null)
                {
                    ModelState.AddModelError("ErrorMessage", "Task ID is Invalid!");
                }

                UserTask task = _mapper.Map<UserTask>(updateDTO);
                task.User_id = userId;

                var usertask = await _userTaskService.UpdateTaskService(task);
                var result = _mapper.Map<TaskDTO>(usertask);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.Result = result;
                return Ok(_response);

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (id == 0)
                {
                    return BadRequest();
                }
                await _userTaskService.DeleteTaskService(id, userId);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }
    }
}
