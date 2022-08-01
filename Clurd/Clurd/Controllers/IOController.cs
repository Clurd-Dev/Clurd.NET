using Clurd.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Clurd.Controllers;

public class IoController : Controller
{
    [HttpPost("api/delete")]
    public bool Delete(IFormCollection value)
    {
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        if (value["username"] == config.username && value["password"] == config.password)
        {
            var io = new IO.IO();
            return io.Delete(value["path"]);
        }
        else
        {
            return false;
        }

    }

    [HttpPost("api/rename")]
    public bool RenameFile(IFormCollection value)
    {
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        var io = new IO.IO();
        if (value["username"] == config.username && value["password"] == config.password)
        {
            return io.Rename(value["old"], value["new"]);
        }
        else
        {
            return false;
        }
        
    }

    [HttpGet("api/space")]
    public string SpaceFree()
    {
        var io = new IO.IO();
        return io.Space();
    }

    [HttpPost("api/copy")]
    public bool copy(IFormCollection value)
    {
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        if (value["username"] == config.username && value["password"] == config.password)
        {
            var io = new IO.IO();
            return io.Copy(value["oldpath"], value["newpath"]);
        }
        else
        {
            return false;
        }

    }

    [HttpPost("api/move")]
    public bool move(IFormCollection value)
    {
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        if (value["username"] == config.username && value["password"] == config.password)
        {
            var io = new IO.IO();
            return io.Move(value["oldpath"], value["newpath"]);
        }
        else
        {
            return false;
        }
    }
    [HttpPost("api/newfolder")]
    public bool createfolder(IFormCollection value){
        var io = new IO.IO();
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        if(value["username"] == config.username && value["password"] == config.password){
            return io.Createfolder(value["path"]);
        }else{
            return false;
        }
        
    }
}