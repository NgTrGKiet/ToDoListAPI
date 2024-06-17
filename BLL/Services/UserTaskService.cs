using BLL.Services.IService;
using DAL.Entites;
using DAL.Repository.IRepository;

namespace BLL.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _usertask;

        public UserTaskService(IUserTaskRepository userTask)
        {
            _usertask = userTask;
        }

        public async Task<List<UserTask>> GetAllTasksService(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Unauthorized");
            }
            return await _usertask.GetAllTasksAsync(userId);
        }

        public async Task<UserTask> GetTaskService(int id, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Unauthorized");
            }

            if (id <= 0)
            {
                throw new Exception("Invalid task ID.");
            }

            var result = await _usertask.GetAsync(u => u.Id == id && u.User_id == userId, tracked: false);
            if(result == null)
            {
                throw new Exception($"Can't get task with {id}");
            }
            return result;
        }

        public async Task CreateTaskService(UserTask task, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Unauthorized");
            }
            if (await _usertask.GetAsync(u=>u.Title.ToLower() == task.Title.ToLower() && u.User_id == userId) != null)
            {
                throw new Exception("Task already exist");
            }
            if(task.Start > task.End)
            {
                throw new Exception("End date cannot be earlier than start date");
            }

            await _usertask.CreateAsync(task);
        }

        public async Task<UserTask> UpdateTaskService(UserTask task)
        {
            if (task.Start > task.End)
            {
                throw new Exception("End date cannot be earlier than start date");
            }
            var result = await _usertask.UpdateAsync(task);
            return result;
        }

        public async Task DeleteTaskService(int id, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Unauthorized");
            }

            var task = await GetTaskService(id, userId);
            if(task == null)
            {
                throw new KeyNotFoundException($"Villa with ID {id} not found");
            }
            await _usertask.RemoveAsync(task);
        }
    }
}
