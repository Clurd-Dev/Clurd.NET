using Clurd.Model;
using Newtonsoft.Json;

namespace Clurd.IO
{
    public class IO
    {
        public bool Rename(string old, string newpath)
        {
            try
            {
                System.IO.File.Move(old, newpath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public string Space()
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return JsonConvert.SerializeObject(diskspace);
        }

        public bool Delete(string path)
        {
            try
            {
                System.IO.File.Delete(path);
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }   
        }

        public string GetFiles(string path)
        {
               try
        {
            var allfiles = Directory.GetFiles(path);
            var alldir = Directory.GetDirectories(path);
            List<FileAPI> allfilesapi = new List<FileAPI>();
            foreach (var fileraw in allfiles)
            {
                var fileinfo = new FileInfo(path + fileraw);
                FileAPI file = new FileAPI();
                file.Name = Path.GetFileName(fileraw);
                file.FullPath = fileinfo.FullName;
                file.Size = fileinfo.Length;
                file.Dir = false;
                file.Creation = Directory.GetCreationTime(path + fileraw);
                file.LastAccess = Directory.GetLastAccessTime(path + fileraw);
                file.LastWrite = Directory.GetLastWriteTime(path + fileraw);
                file.ReadOnlyFil = fileinfo.IsReadOnly;
                allfilesapi.Add(file);
            }

            foreach (var dir in alldir)
            {
                FileAPI file = new FileAPI();
                file.Name = Path.GetFileName(dir);
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
}
