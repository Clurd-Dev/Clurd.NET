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
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                return true;
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex);
                return false;
            }   
        }

        public string GetFiles(string path)
        {
               try
        {
                var pathraw = System.IO.File.ReadAllText("./config.json");

#pragma warning disable CS8602 // ignore null warning.
                if (path.Length >= JsonConvert.DeserializeObject<Config>(pathraw).path.Length)
                {
                     var allfiles = Directory.GetFiles(path);
                                var alldir = Directory.GetDirectories(path);
                                List<FileAPI> allfilesapi = new List<FileAPI>();
                                foreach (var fileraw in allfiles)
                                {
                                    var fileinfo = new FileInfo(fileraw);
                                    FileAPI file = new FileAPI();
                                    file.Name = Path.GetFileName(fileraw);
                                    file.FullPath = fileinfo.FullName.Replace('\\', '/');
                                    file.Size = fileinfo.Length;
                                    file.Dir = false;
                                    file.Creation = Directory.GetCreationTime(fileraw);
                                    file.LastAccess = Directory.GetLastAccessTime(fileraw);
                                    file.LastWrite = Directory.GetLastWriteTime(fileraw);
                                    file.ReadOnlyFil = fileinfo.IsReadOnly;
                                    allfilesapi.Add(file);
                                }

                                foreach (var dir in alldir)
                                {
                                    FileAPI file = new FileAPI();
                                    file.Name = Path.GetFileName(dir);
                                    file.FullPath = Path.GetFullPath(dir).Replace('\\', '/');
                                    file.Size = 0;
                                    file.Dir = true;
                                    allfilesapi.Add(file);
                                }

                                return JsonConvert.SerializeObject(allfilesapi);
                }
                else
                {
                    return "0";
                }
#pragma warning restore CS8602 // Dereferenziamento di un possibile riferimento Null.
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "[]";
        }
        }
        public bool Copy(string oldpath, string newpath)
        {
            try
            {
                System.IO.File.Copy(oldpath, newpath);
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool Move(string oldpath, string newpath)
        {
            try
            {
                System.IO.File.Move(oldpath, newpath);
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
