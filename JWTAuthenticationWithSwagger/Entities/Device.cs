using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Entities
{
    public class Device
    {
        public string DeviceId { get; set; } 
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public string RigId { get; set; }
        public bool Active { get; set; }
        public virtual Rig Rig { get; set; }
        public List<DeviceStats> DeviceStats { get; set; }
        public ICollection<DeviceGroup> DeviceGroups { get; set; }
    }
}
