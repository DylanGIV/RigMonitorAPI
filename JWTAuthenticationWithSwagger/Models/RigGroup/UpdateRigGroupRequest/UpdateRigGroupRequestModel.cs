using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.RigGroup.UpdateRigGroupRequest
{
    public class UpdateRigGroupRequestModel
    {
        public long? RigGroupId { get; set; }
        public string RigGroupName { get; set; }
        public string RigGroupDescription { get; set; }
        public List<string> RigIds { get; set; }
    }
}
