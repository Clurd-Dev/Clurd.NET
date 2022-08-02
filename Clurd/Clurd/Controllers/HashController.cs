using Microsoft.AspNetCore.Mvc;
using Standart.Hash.xxHash;
using Clurd.Model;
using Newtonsoft.Json;

namespace Clurd.Controllers
{
    public class HashController : Controller
    {
      [HttpPost("api/hash")]
      public ulong Hash(IFormCollection value){
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);

        if (value["username"] == config.username && value["password"] == config.password){
          var path = value["path"].ToString().Replace("|", "/");
          var bytes = System.IO.File.ReadAllBytes(path);
          var hash = xxHash3.ComputeHash(bytes, bytes.Length);
          return hash;  
        }else{
          return 0;
        }

      }
    }
}