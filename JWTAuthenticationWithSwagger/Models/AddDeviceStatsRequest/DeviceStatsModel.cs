using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.AddDeviceStatsRequest
{
    public class DeviceStatsModel
    {
        public string DeviceId { get; set; }
        public short Temperature { get; set; }
        public decimal PowerUsage { get; set; }
        public short FanSpeed { get; set; }
        public short MemoryClockSpeed { get; set; }
        public short CoreClockSpeed { get; set; }
        public short DeviceUsage { get; set; }
    }
}
