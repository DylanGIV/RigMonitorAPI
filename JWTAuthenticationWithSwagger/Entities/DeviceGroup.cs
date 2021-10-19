using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Entities
{
    public class DeviceGroup
    {
        public long DeviceGroupId { get; set; }
        public string DeviceGroupName { get; set; }
        public string DeviceGroupDescription { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
