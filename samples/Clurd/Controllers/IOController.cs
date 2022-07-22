using Microsoft.AspNetCore.Mvc;

namespace Clurd.Controllers;

public class IoController : Controller
{
    [HttpPost("api/delete")]
    public bool Delete(IFormCollection value)
    {
        var io = new IO.IO();
        return io.Delete(value["path"]);
    }

    [HttpPost("api/rename")]
    public bool RenameFile(IFormCollection value)
    {
        var io = new IO.IO();
        return io.Rename(value["old"], value["new"]);
    }

    [HttpGet("api/space")]
    public string SpaceFree()
    {
        var io = new IO.IO();
        return io.Space();
    }
}