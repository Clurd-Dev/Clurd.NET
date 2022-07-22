using Microsoft.AspNetCore.Mvc;
namespace Clurd.Controllers;

public class FilesController : Controller
{
    [HttpPost("api/files")]
    public string Index(IFormCollection value)
    {
        var io = new IO.IO();
        return io.GetFiles(value["path"]);
    }
}