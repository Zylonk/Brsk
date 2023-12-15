using ClientWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AuthUser(User user)
        {
            string apiUrl = "http://localhost:7036/api/login";
            user.Id = "def";

            using (HttpClient httpClient = new HttpClient())
            {
                string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string token = await response.Content.ReadAsStringAsync();
                        Token.Tokens = token;
                        return RedirectToAction("Index", "TasksWork");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        var errors = ($"Error: {response.StatusCode} - {response.ReasonPhrase}. Additional information: {errorContent}");
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            return BadRequest();
        }
    }
}