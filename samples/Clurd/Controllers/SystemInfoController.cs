using Microsoft.AspNetCore.Mvc;
using Clurd.Methods;

namespace Clurd.Controllers;

public class SystemInfoController
{

    [HttpGet("api/systeminfo")]
    public string Get()
    {
        var systeminfo = new GetSystemInfo();
        return systeminfo.Info();
    }
}