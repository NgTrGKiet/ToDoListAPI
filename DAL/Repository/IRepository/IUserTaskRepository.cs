using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    public interface IUserTaskRepository
    {
        Task<List<UserTask>> GetAllTasksAsync();
        Task<UserTask> GetAsync(Expression<Func<UserTask, bool>> filter = null, bool tracked = true);
        Task CreateAsync(UserTask entity);
        Task RemoveAsync(UserTask entity);
        Task <UserTask> UpdateAsync (UserTask entity);
        Task SaveAsync();
    }
}
