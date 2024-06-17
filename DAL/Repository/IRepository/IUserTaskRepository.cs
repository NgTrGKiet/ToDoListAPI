using DAL.Entites;
using System.Linq.Expressions;

namespace DAL.Repository.IRepository
{
    public interface IUserTaskRepository
    {
        Task<List<UserTask>> GetAllTasksAsync(string userId);
        Task<UserTask> GetAsync(Expression<Func<UserTask, bool>> filter = null, bool tracked = true);
        Task CreateAsync(UserTask entity);
        Task RemoveAsync(UserTask entity);
        Task <UserTask> UpdateAsync (UserTask entity);
        Task SaveAsync();
    }
}
