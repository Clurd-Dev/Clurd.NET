using Clurd.Model;
using Microsoft.AspNetCore.Mvc;
using Hardware.Info;
using Newtonsoft.Json;

namespace Clurd.Controllers;

public class SystemInfoController
{
    static readonly IHardwareInfo hardwareInfo = new HardwareInfo();

    [HttpGet("api/systeminfo")]
    public string Get()
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