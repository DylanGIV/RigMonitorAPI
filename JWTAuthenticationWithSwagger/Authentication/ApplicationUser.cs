using Microsoft.AspNetCore.Identity;
using RigMonitorAPI.Entities;
using System.Collections.Generic;

namespace RigMonitorAPI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public List<Rig> Rigs { get; set; }
    }
}
