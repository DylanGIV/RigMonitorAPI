using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Models.AddWalletAddressRequest
{
    public class AddWalletAddressRequestModel
    {
        public string Address { get; set; }
        public string PoolId { get; set; }
        public string PoolName { get; set; }
    }
}
