using BLL.Model;
using DAL.Entities;
namespace BLL.Services.IService
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetAllTasksService(string userId, FilterModel filter = null);
        Task<UserTask> GetTaskService(int id, string userId);
        Task CreateTaskService(UserTask task, string userId);
        Task<UserTask> UpdateTaskService(UserTask task);
        Task DeleteTaskService(int id, string userId);
    }
}
