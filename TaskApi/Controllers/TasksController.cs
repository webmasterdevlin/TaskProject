using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TaskApi.Contracts;

using TaskApi.Helpers;
using TaskApi.Entities;
using TaskApi.Dtos;

namespace TaskApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {

        private readonly ILogger<TasksController> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _repo;

        public TasksController(ITaskRepository repo, ILogger<TasksController> logger, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Tasks
        [HttpGet]
        public IActionResult GetTaskEntity()
        {
            var tasksFromRepo = _repo.GetAllTasks();

            #region Manual Mapping
            //            var tasks = new List<TaskDto>();
            //            foreach (var task in tasksFromRepo)
            //            {
            //                tasks.Add(new TaskDto
            //                {
            //                    Id = task.Id,
            //                    Title = task.Title,
            //                    IsDone = task.IsDone,
            //                    DaysRemaining = task.DeadLine.GetDeadLine()
            //                });
            //            }
            #endregion

            var tasks = _mapper.Map<IEnumerable<TaskEntity>, IEnumerable<TaskDto>>(tasksFromRepo);


            return new JsonResult(tasks);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskEntity([FromRoute] Guid id)
        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

            var taskEntity = await _repo.FindTask(id);

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
            if (id != taskEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repo.UpdateTask(taskEntity);
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
            await _repo.AddTask(taskEntity);

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

            await _repo.DeleteTask(id);

            _logger.LogInformation(100, $"Task {id} was deleted");

            return Ok();
        }

        private async Task<bool> TaskEntityExists(Guid id)
        {
            return await _repo.Exists(id);
        }
    }
}