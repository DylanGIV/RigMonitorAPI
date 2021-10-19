using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.DeviceGroup.AddDeviceGroupRequest
{
    public class AddDeviceGroupRequestModel
    {
        public string DeviceGroupName { get; set; }
        public string DeviceGroupDescription { get; set; }
    }
}
