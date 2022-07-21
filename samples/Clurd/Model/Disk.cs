namespace Clurd.Model;

public class Disk
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public string Label { get; set; } = "";
    public string Fs { get; set; } = "";
    public long FreeSpace { get; set; } = 0;
    public long TotalFreespace { get; set; } = 0;
    public long TotalSize { get; set; } = 0;
    
}