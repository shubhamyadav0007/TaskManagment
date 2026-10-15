using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskBackend.Models;
using TaskBackend.Repository;

namespace TaskBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IGenericRepo<Models.Task> taskRepo;
        private readonly UserManager<ApplicationUser> userManager;
        public AdminController(IConfiguration configuration, IGenericRepo<Models.Task> taskRepo, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.taskRepo = taskRepo;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet("GetAllTask")]
        public async Task<IActionResult> GetAllTask()
        {
            var tasks = await taskRepo.GetAllAsync(
                t => t.CreatedBy,
                t => t.AssignedUsers
            );

            if (tasks == null || !tasks.Any())
            {
                return Ok(new List<TaskViewModel>());
            }

            
            var taskViewModels = tasks.Select(t => new TaskViewModel
            {
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedById = t.CreatedById,
                AssignedUserIds = t.AssignedUsers.Select(u => u.Id),
                AssignedUserNames = t.AssignedUsers.Select(u => u.UserName)
            }).ToList();
           
            return Ok(taskViewModels);
        }
        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await taskRepo.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await taskRepo.DeleteAsync(id);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPut("UpdateTask/{id}")]
        public async Task<IActionResult> UpdateTask(int id,TaskViewModel updatedTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTask = await taskRepo.GetByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            
            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.CreatedById = updatedTask.CreatedById;
            var assignedUsers = new List<ApplicationUser>();
            foreach (var userId in updatedTask.AssignedUserIds)
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    assignedUsers.Add(user);
                }
            }
            existingTask.AssignedUsers = assignedUsers;

            await taskRepo.UpdateAsync(existingTask);
            return Ok();
        }
        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(TaskViewModel newTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assignedUsers = new List<ApplicationUser>();
            foreach (var userId in newTask.AssignedUserIds)
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    assignedUsers.Add(user);
                }
            }

            var task = new Models.Task
            {
                Title = newTask.Title,
                Description = newTask.Description,
                Status = newTask.Status,
                CreatedById = newTask.CreatedById,
                AssignedUsers = assignedUsers
            };

            await taskRepo.InsertAsync(task);
            return Ok();
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = userManager.Users.ToList(); 

            if (users == null || !users.Any())
            {
                return NotFound();
            }
            var userViewModels = users.Select(u => new ApplicationUserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
            }).ToList();
            return Ok(userViewModels);
        }



    }
}
