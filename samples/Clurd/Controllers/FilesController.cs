using Microsoft.AspNetCore.Mvc;
using Clurd.Model;
using Newtonsoft.Json;
namespace Clurd.Controllers;

public class FilesController : Controller
{
    [HttpPost("api/files")]
    public string Index(IFormCollection value)
    {
        try
        {
            var allfiles = Directory.GetFiles(value["path"]);
            var alldir = Directory.GetDirectories(value["path"]);
            List<FileAPI> allfilesapi = new List<FileAPI>();
            foreach (var fileraw in allfiles)
            {
                var fileinfo = new FileInfo(value["path"] + fileraw);
                FileAPI file = new FileAPI();
                file.Name = fileraw;
                file.FullPath = fileinfo.FullName;
                file.Size = fileinfo.Length;
                file.Dir = false;
                file.Creation = Directory.GetCreationTime(value["path"] + fileraw);
                file.LastAccess = Directory.GetLastAccessTime(value["path"] + fileraw);
                file.LastWrite = Directory.GetLastWriteTime(value["path"] + fileraw);
                file.ReadOnlyFil = fileinfo.IsReadOnly;
                allfilesapi.Add(file);
            }

            foreach (var dir in alldir)
            {
                FileAPI file = new FileAPI();
                file.Name = dir;
                file.FullPath = "";
                file.Size = 0;
                file.Dir = true;
                allfilesapi.Add(file);
            }

            return JsonConvert.SerializeObject(allfilesapi);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "[]";
        }
       
    }
}