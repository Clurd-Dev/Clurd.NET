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

    [HttpPost("api/copy")]
    public bool copy(IFormCollection value)
    {
        var io = new IO.IO();
        return io.Copy(value["oldpath"], value["newpath"]);
    }

    [HttpPost("api/move")]
    public bool move(IFormCollection value)
    {
        var io = new IO.IO();
        return io.Move(value["oldpath"], value["newpath"]);
    }
}