using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Entities
{
    public class RigGroup
    {
        public long RigGroupId { get; set; }
        public string RigGroupName { get; set; }
        public string RigGroupDescription { get; set; }

        public ICollection<Rig> Rigs { get; set; }
    }
}
