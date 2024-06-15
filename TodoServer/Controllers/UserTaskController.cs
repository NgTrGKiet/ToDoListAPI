using AutoMapper;
using BLL.Model;
using BLL.Services.Iservice;
using DAL.Entites;
using DAL.Entites.DTO;
using DAL.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TodoServer.Controllers
{
    [ApiController]
    [Route("api/usertask")]
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _usertask;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public UserTaskController(IUserTaskService usertask, IMapper mapper)
        {
            _usertask = usertask;
            this._response = new();
            _mapper = mapper;
        }

        [HttpGet("all-task")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetTasks()
        {
            try
            {
                IEnumerable<UserTask> taskLists = await _usertask.GetAllTasksService();
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
            }
            return _response;
        }

        [HttpGet("single-task/{id:int}", Name = "GetTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTask(int id)
        {
            try
            {
                var task = await _usertask.GetTaskService(id);

                if (task == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }
                _response.Result = _mapper.Map<TaskDTO>(task);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            } catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateTask([FromForm] TaskCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                UserTask task = _mapper.Map<UserTask>(createDTO);
                task.user_id = "1";

                await _usertask.CreateTaskService(task);

                _response.Result = _mapper.Map<TaskDTO>(task);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetTask", new { id = task.Id }, task);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateTask(int id, [FromBody] UpdateTaskDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id) 
                {
                    return BadRequest();
                }

                if(await _usertask.GetTaskService(id) ==  null)
                {
                    ModelState.AddModelError("ErrorMessage", "Task ID is Invalid!");
                }

                UserTask task = _mapper.Map<UserTask>(updateDTO);
                task.user_id = "1";

                var result = await _usertask.UpdateTaskService(task);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.Result = result;
                return Ok(_response);

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteTask(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                await _usertask.DeleteTaskService(id);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }
    }
}
