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

    [HttpPost("api/upload/{path}")]
    public async Task<bool> Upload(string path)
    {
        try
        {
            var files = HttpContext.Request.Form.Files;
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
}