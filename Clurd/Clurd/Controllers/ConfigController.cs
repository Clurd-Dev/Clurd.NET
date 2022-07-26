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
            var path = System.IO.File.ReadAllText("./config.json");
#pragma warning disable CS8603
            return JsonConvert.DeserializeObject<Config>(path).path;
#pragma warning restore CS8603
        }
    }
}
    