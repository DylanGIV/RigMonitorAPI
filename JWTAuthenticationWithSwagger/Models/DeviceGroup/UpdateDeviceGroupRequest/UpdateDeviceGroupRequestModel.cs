using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.DeviceGroup.UpdateDeviceGroupRequest
{
    public class UpdateDeviceGroupRequestModel
    {
        public long? DeviceGroupId { get; set; }
        public string DeviceGroupName { get; set; }
        public string DeviceGroupDescription { get; set; }
        public List<string> DeviceIds { get; set; }
    }
}
