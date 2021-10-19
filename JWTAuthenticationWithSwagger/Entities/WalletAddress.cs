using RigMonitorAPI.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Entities
{
    public class WalletAddress
    {
        public string Address { get; set; }
        public string PoolId { get; set; }
        public string PoolName { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
