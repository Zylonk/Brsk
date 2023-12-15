using Apis.Class;
using Apis.Model;
using Apis.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Apis.DatabaseContext
{
    [Authorize]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static JsonSerializerSettings _ignoreLoopHandling = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        [HttpGet]
        [Route("/api/tasks")]
        public ActionResult<List<Apis.Model.Task>> TaskGetInfo()
        {
            List<Apis.Model.Task> task = GetrContext.Context.Tasks.ToList();
            if (task.Count != 0)
            {
                return Content(JsonConvert.SerializeObject(task, _ignoreLoopHandling));
            }
            return BadRequest("Bd not connect");
        }

        [HttpPost]
        [Route("/api/tasks")]
        public IActionResult AddTask([FromBody] TasksDTO task)
        {
            var currentUser =  GetCurrentUser();
            task.Id = Guid.NewGuid().ToString();
            var s = currentUser.Id.ToString();
            task.UserId = currentUser.Id;
            GetrContext.Context.Tasks.Add(TasksDTO.TaskConverter(task));
            GetrContext.Context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("/api/tasks/{taskId}")]
        public IActionResult RemoveTask(string taskId)
        {
            var task = GetrContext.Context.Tasks.ToList().FirstOrDefault(x => x.Id == taskId);
            if (task != null)
            {
                GetrContext.Context.Tasks.Remove(task);
                GetrContext.Context.SaveChanges();
                return Ok("Pass");
            }
            return BadRequest("Fail remove");
        }
        [HttpPut]
        [Route("/api/tasks/{taskId}")]
        public IActionResult PutTask(string taskId, [FromBody] TasksDTO newTask)
        {
            var oldTask = GetrContext.Context.Tasks.ToList().FirstOrDefault(x => x.Id == taskId);
            if (oldTask != null)
            {
                oldTask.Id = taskId;
                oldTask.Description = newTask.Description;
                oldTask.Timeframe = newTask.Timeframe;
                oldTask.Priority = newTask.Priority;
                oldTask.Done = newTask.Done;
                GetrContext.Context.SaveChanges();
                return Ok("Pass");
            }
            return BadRequest("Fail put");
        }
        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    Id = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value
                };
            }
            return null;
        }
    }
}
