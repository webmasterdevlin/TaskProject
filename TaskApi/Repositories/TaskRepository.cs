using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApi.Contracts;
using TaskApi.Data;
using TaskApi.Entities;

namespace TaskApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Exists(Guid id)
        {
            return await _context.Tasks.AnyAsync(e => e.Id == id);
        }

        public IEnumerable<TaskEntity> GetAllTasks()
        {
            return _context.Tasks;
        }

        public async Task<TaskEntity> FindTask(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskEntity> AddTask(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> UpdateTask(TaskEntity task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> DeleteTask(Guid id)
        {
            var task = await _context.Tasks.SingleOrDefaultAsync(e => e.Id == id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
