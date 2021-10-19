using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.AddDeviceRequest
{
    public class DeviceModel
    {
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public string DeviceId { get; set; }
        public long RigId { get; set; }
    }
}
