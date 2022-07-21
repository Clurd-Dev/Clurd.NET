using Clurd.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Clurd.Controllers;

public class IoController : Controller
{
    [HttpPost("api/delete")]
    public bool Delete(IFormCollection value)
    {
        try
        {
            System.IO.File.Delete(value["path"]);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    [HttpGet("api/space")]
    public string SpaceFree()
    {
        try
        {
            var disks = DriveInfo.GetDrives();
            List<Disk> diskspace = new List<Disk>();
            try
            {
                foreach (var disk in disks)
                {
                    var diskapi = new Disk();
                    diskapi.Name = disk.Name;
                    diskapi.Type = disk.DriveType.ToString();
                    diskapi.TotalSize = disk.TotalSize;
                    diskapi.TotalFreespace = disk.TotalFreeSpace;
                    diskapi.FreeSpace = disk.TotalFreeSpace;
                    diskapi.Fs = disk.DriveFormat;
                    diskapi.Label = disk.VolumeLabel;
                    diskspace.Add(diskapi);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return JsonConvert.SerializeObject(diskspace);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "[]";
        }
    }
}