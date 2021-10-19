using RigMonitorAPI.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Entities
{
    public class Rig
    {
        public long RigId { get; set; }
        public string RigName { get; set; }
        public string RigDescription { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public List<Device> Devices { get; set; }

        public ICollection<RigGroup> RigGroups { get; set; }

    }
}
    