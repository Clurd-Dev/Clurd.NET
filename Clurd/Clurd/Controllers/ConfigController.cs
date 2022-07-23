using Microsoft.AspNetCore.Mvc;
namespace Clurd.Controllers
{
    public class ConfigController : Controller
    {
        [HttpGet("api/config")]
        public string Getpath()
        {
#pragma warning disable CS8603
            return System.Configuration.ConfigurationManager.AppSettings.Get("Path");
#pragma warning restore CS8603
        }
    }
}
    