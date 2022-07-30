using Clurd.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Clurd.Controllers;

public class FilesController : Controller
{

    [HttpPost("api/files")]
    public string Index(IFormCollection value)
    {
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        if (value["password"] == config.password && value["username"] == config.username)
        {
            var io = new IO.IO();
            return io.GetFiles(value["path"]);
        }
        else
        {
            return "Invalid user or password";
        }

    }

    [HttpPost("api/file")]
    public async Task<FileStreamResult?> GetFile(IFormCollection value){
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        if (value["password"] == config.password && value["username"] == config.username)
        {
           try{
                    var file = await System.IO.File.ReadAllBytesAsync(value["path"]);
                    Stream stream = new MemoryStream(file);
                    return File(stream, "application/octet-stream");
                }catch(Exception ex){
                    Console.WriteLine(ex);
                    return null;
                }
        }
        else
        {
            return null;
        }
     

    }

    [DisableRequestSizeLimit]
    [HttpPost("api/upload")]
    public async Task<bool> Upload()
    {
        var configstring = System.IO.File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(configstring);
        var username = HttpContext.Request.Query["username"].ToString();
        var password = HttpContext.Request.Query["password"].ToString();
        if (username == config.username && password == config.password)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                var path = HttpContext.Request.Query["path"].ToString();
                path = path.Replace("|", "/");
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {

                        //var uploads = Path.Combine(_environment.WebRootPath, "files");
                        string FileName = file.FileName;
                        Console.WriteLine(FileName);
                        using (var fileStream = new FileStream(Path.Combine(path, FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        else
        {
            return false;
        }


    }
}