namespace Clurd.Model;

public class FileAPI
{
    public string Name { get; set; } = "";
    public long Size { get; set; } = 0;
    public string FullPath { get; set; } = "";
    public bool? Dir { get; set; } = null;
    public DateTime? LastAccess { get; set; } = null;
    public DateTime? LastWrite { get; set; } = null;
    public DateTime? Creation { get; set; } = null;
    public bool? ReadOnlyFil { get; set; } = null;
}