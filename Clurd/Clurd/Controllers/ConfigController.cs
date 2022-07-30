using Clurd.Methods;
using Clurd.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            var configstring = System.IO.File.ReadAllText("./config.json");
            var config = JsonConvert.DeserializeObject<Config>(configstring);
            if (value["oldpass"] == config.password && value["olduser"] == config.username)
            {
                var updater = new ConfigUpdater();
                return updater.UpdateConfig(value["path"], value["username"], value["password"]);
            }
            else
            {
                return false;
            }

        }
        
    }
}
    