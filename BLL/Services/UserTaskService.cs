using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services.IService;
using DAL.Entites;
using DAL.Entities.DTO;
using DAL.Repository;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _usertask;

        public UserTaskService(IUserTaskRepository userTask)
        {
            _usertask = userTask;
        }

        public async Task<List<UserTask>> GetAllTasksService()
        {
            return await _usertask.GetAllTasksAsync();
        }

        public async Task<UserTask> GetTaskService(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid task ID.");
            }
            return await _usertask.GetAsync(u => u.Id == id, tracked: false);
        }

        public async Task CreateTaskService(UserTask task)
        {
            if(await _usertask.GetAsync(u=>u.title.ToLower() == task.title.ToLower()) != null)
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

        public async Task DeleteTaskService(int id)
        {
            var task = await GetTaskService(id);
            if(task == null)
            {
                throw new KeyNotFoundException($"Villa with ID {id} not found");
            }
            await _usertask.RemoveAsync(task);
        }
    }
}
