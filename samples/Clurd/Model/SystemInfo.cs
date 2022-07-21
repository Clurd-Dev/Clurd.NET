using Hardware.Info;

namespace Clurd.Model;

public class SystemInfo
{
    public MemoryStatus MemoryStatus { get; set; } = null;
    public List<CPU> Cpus { get; set; } = null;
    public List<Memory> Memories { get; set; } = null;
    public List<Motherboard> Motherboards { get; set; } = null;
    public List<NetworkAdapter> NetworkAdapters { get; set; } = null;
    public List<BIOS> Bioses { get; set; } = null;
}