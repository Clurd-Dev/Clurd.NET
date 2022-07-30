using Clurd.Methods;
using Microsoft.AspNetCore.Mvc;

namespace Clurd.Controllers
{
    public class ConfigController : Controller
    {
        [HttpGet("api/config")]
        public string Getpath()
        {
            return System.IO.File.ReadAllText("./config.json");
        }

        [HttpPost("api/config")]
        public bool Update(IFormCollection value)
        {
            var updater = new ConfigUpdater();
            return updater.UpdateConfig(value["path"], value["username"], value["password"]);
        }
        
    }
}
    