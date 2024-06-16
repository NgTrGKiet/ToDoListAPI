using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IService
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetAllTasksService(string userId);
        Task<UserTask> GetTaskService(int id, string userId);
        Task CreateTaskService(UserTask task);
        Task<UserTask> UpdateTaskService(UserTask task);
        Task DeleteTaskService(int id, string userId);
    }
}
