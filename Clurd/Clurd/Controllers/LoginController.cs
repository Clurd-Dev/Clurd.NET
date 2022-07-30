using Microsoft.AspNetCore.Mvc;
using Clurd.Methods;

namespace Clurd.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost("api/login")]
        public bool login(IFormCollection value)
        {
            var logger = new Login();
            return logger.login(value["user"], value["password"]);
        }
    }
}
