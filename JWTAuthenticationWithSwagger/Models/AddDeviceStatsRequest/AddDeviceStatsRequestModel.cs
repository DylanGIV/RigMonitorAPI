using RigMonitorAPI.Models.AddDeviceStatsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.Request
{
    public class AddDeviceStatsRequestModel
    {
        public DateTime Timestamp { get; set; }
        public List<DeviceStatsModel> DevicesStats { get; set; }
    }
}
