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

    [HttpPost("api/file")]
    public async Task<FileStreamResult?> GetFile(IFormCollection value){
        try{
            // Need to add password support
            var file = await System.IO.File.ReadAllBytesAsync(value["path"]);
            Stream stream = new MemoryStream(file);
            return File(stream, "application/octet-stream");
        }catch(Exception ex){
            Console.WriteLine(ex);
            return null;
        }

    }
}