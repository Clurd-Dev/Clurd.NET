using Clurd.Model;
using Hardware.Info;
using Newtonsoft.Json;

namespace Clurd.Methods
{
    public class GetSystemInfo
    {
        static readonly IHardwareInfo hardwareInfo = new HardwareInfo();

        public string Info()
        {
            try
            {
                hardwareInfo.RefreshAll();
                var information = new SystemInfo();
                information.MemoryStatus = hardwareInfo.MemoryStatus;
                information.Bioses = hardwareInfo.BiosList;
                information.Cpus = hardwareInfo.CpuList;
                information.Memories = hardwareInfo.MemoryList;
                //information.NetworkAdapters = hardwareInfo.NetworkAdapterList; // TOFIXED
                information.Motherboards = hardwareInfo.MotherboardList;
                return JsonConvert.SerializeObject(information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "[]";
            }
        }
    }
}
