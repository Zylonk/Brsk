using ClientWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text;

namespace ClientWeb.Controllers
{
    public class TasksWorkController : Controller
    {
        string _apiUrl = "http://localhost:7036/api/tasks";
        public async Task<IActionResult> Index()
        {
           
            List<Tasks> tasks = await GetrListTask();
            ViewBag.Tasks = tasks;
            ViewBag.UserID = "DASDASDDAS";
            return View();
        }

        private async Task<List<Tasks>> GetrListTask()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.Tokens);

                HttpResponseMessage response = await httpClient.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<Tasks> tasks = JsonConvert.DeserializeObject<List<Tasks>>(jsonResponse);
                    return tasks;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
        }
        [HttpGet]
        public IActionResult Addtask()
        {
            return View();
        }
        
         [HttpPost]
        public async Task<IActionResult> Addtask(Tasks task, string done)
        {
             task.Id = "def";
            task.UserId = "def";
            if (done == "Да")
            {
                task.Done = true;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.Tokens);
                string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(task);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
               
                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync(_apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "TasksWork");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        var errors = ($"Error: {response.StatusCode} - {response.ReasonPhrase}. Additional information: {errorContent}");
                        return RedirectToAction("AddTask", "TasksWork");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<ActionResult> PutTask(string id)
        {
            List<Tasks> mc = await GetrListTask();
            var info = mc.ToList().FirstOrDefault(x => id == x.Id);
            ViewBag.Description = info.Description;
            ViewBag.Timeframe = info.Timeframe;
            return View(info);
        }
      
        public async Task<IActionResult> PutsTask(Tasks task, string done)
        { 
          task.UserId = "def";
            if (done == "Да")
            {
                task.Done = true;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.Tokens);
                string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(task);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await httpClient.PutAsync($"{_apiUrl}/{task.Id}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "TasksWork");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        var errors = ($"Error: {response.StatusCode} - {response.ReasonPhrase}. Additional information: {errorContent}");
                        return RedirectToAction("PutTask", "TaskWork");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            return BadRequest();
        }
   
        public async Task<IActionResult> RemoveTask(string id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.Tokens);
                // Отправьте DELETE запрос
                HttpResponseMessage response = await httpClient.DeleteAsync($"{_apiUrl}/{id}");

                // Проверьте успешность запроса
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "TasksWork");
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    var errors = ($"Error: {response.StatusCode} - {response.ReasonPhrase}. Additional information: {errorContent}");
                    return RedirectToAction("PutTask", "TaskWork");
                }
            }
        }
    }
}
