using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Entities;

namespace TaskApi.Contracts
{
    public interface ITaskRepository
    {
        Task<bool> Exists(Guid id);
        IEnumerable<TaskEntity> GetAllTasks();
        Task<TaskEntity> FindTask(Guid id);
        Task<TaskEntity> AddTask(TaskEntity task);
        Task<TaskEntity> UpdateTask(TaskEntity task);
        Task<TaskEntity> DeleteTask(Guid id);
    }
}
