using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApi.Contracts;
using TaskApi.Models;

namespace TaskApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly ITaskRepository _tasks;

        public TasksController(ITaskRepository tasks)
        {
            _tasks = tasks;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<TaskEntity> GetTaskEntity()
        {
            return _tasks.GetAllTasks();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskEntity([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskEntity = await _tasks.FindTask(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            return Ok(taskEntity);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskEntity([FromRoute] Guid id, [FromBody] TaskEntity taskEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _tasks.UpdateTask(taskEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TaskEntityExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> PostTaskEntity([FromBody] TaskEntity taskEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tasks.AddTask(taskEntity);

            return CreatedAtAction("GetTaskEntity", new { id = taskEntity.Id }, taskEntity);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskEntity([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await TaskEntityExists(id))
            {
                return NotFound();
            }

            await _tasks.DeleteTask(id);
            return Ok();
        }

        private async Task<bool> TaskEntityExists(Guid id)
        {
            return await _tasks.Exists(id);
        }
    }
}