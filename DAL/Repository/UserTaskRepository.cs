using DAL.Entites;
using DAL.Entites.DTO;
using DAL.Data;
using DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DAL.Repository
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<UserTask> _dbSet;

        public UserTaskRepository(ApplicationDbContext db)
        {
            _db = db;
            this._dbSet = _db.Set<UserTask>();
        }

        public async Task<List<UserTask>> GetAllTasksAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<UserTask> GetAsync(Expression<Func<UserTask, bool>> filter = null, bool tracked = true)
        {
            IQueryable<UserTask> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //var result = await query.FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync(); ;
        }
        public async Task CreateAsync(UserTask entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }
        public async Task RemoveAsync(UserTask entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }
        public async Task<UserTask> UpdateAsync(UserTask entity)
        {
            _dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
