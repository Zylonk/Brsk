using ClientWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ClientWeb.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RegistrUser(User user)
        {
            string apiUrl = "http://localhost:7036/api/registers";
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
                        string successContent = await response.Content.ReadAsStringAsync();
                      
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        var errors = ($"Error: {response.StatusCode} - {response.ReasonPhrase}. Additional information: {errorContent}");
                        return RedirectToAction("Index", "Registration");
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

