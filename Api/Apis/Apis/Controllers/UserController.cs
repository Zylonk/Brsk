using Apis.Class;
using Apis.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Apis.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        private static JsonSerializerSettings _ignoreLoopHandling = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public IActionResult UserLogin([FromBody] User user)
        {
            var userList = GetrContext.Context.Users.ToList();
            foreach (var i in userList)
            {
                if (user.Username == i.Username && BCrypt.Net.BCrypt.Verify(user.PasswordHash,i.PasswordHash))
                {
                    var token = GenerateToken(i.Username, i.Id );
                    return Ok(token);
                }
            }
            ModelState.AddModelError(string.Empty, "Неверные учетные данные");
            return BadRequest(ModelState);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/registers")]
        public IActionResult UserRegistrations([FromBody] User user)
        {
            var check =  GetrContext.Context.Users.FirstOrDefault(x => x.Username == user.Username);
            if (check == null)
            {
                user.Id = Guid.NewGuid().ToString();
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                GetrContext.Context.Users.Add(user);
                GetrContext.Context.SaveChanges();
                return Ok(new { Message = "Регистрация успешна! Теперь вы можете войти." });
            }
            return BadRequest("Login's taken");
        }

        [HttpGet]
        [Route("api/login")]
        public ActionResult<List<User>> UserGetInfo()
        {
            List<User> user = GetrContext.Context.Users.ToList();
            if (user.Count != 0)
            {
                return Content(JsonConvert.SerializeObject(user, _ignoreLoopHandling));
            }
            return BadRequest("Bd not connect");
        }
        [HttpGet]
        [Route("api/test")]
        public IActionResult test()
        {
            return Ok("Token successfully validated");
        }
        private string GenerateToken(string userName, string id )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier,id.ToString()),
            new Claim(ClaimTypes.GivenName,userName)
            };
            var token = new JwtSecurityToken(
                _config.GetSection("Jwt:Issuer").Value, _config.GetSection("Jwt:Audience").Value,
                claims,
                expires: DateTime.Now.AddMinutes(55),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
