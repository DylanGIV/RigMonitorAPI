using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.Request
{
    public class AddRigRequestModel
    {
        public string RigName { get; set; }
        public string RigDescription { get; set; }
    }
}
