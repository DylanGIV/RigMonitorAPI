using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RigMonitorAPI.Authentication;
using RigMonitorAPI.Entities;
using RigMonitorAPI.Models.AddRigGroupRequest;
using RigMonitorAPI.Models.RigGroup.UpdateRigGroupRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RigMonitorAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RigGroupController : Controller
    {
        private readonly ILogger<RigGroupController> _logger;
        private readonly ApplicationDbContext _context;

        public RigGroupController(ILogger<RigGroupController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<RigGroup>> PostRigGroup([FromBody] AddRigGroupRequestModel addRigGroup)
        {
            var rigGroup = new RigGroup();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var rigs = _context.Rig.Where(r => r.UserId == userId).ToList();

            rigGroup.RigGroupName = addRigGroup.RigGroupName;
            rigGroup.RigGroupDescription = addRigGroup.RigGroupDescription;

            _context.RigGroup.Add(rigGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRigGroups), rigGroup);
        }

        [HttpPut]
        public async Task<ActionResult<RigGroup>> PutRigGroup([FromBody] UpdateRigGroupRequestModel updateRigGroup)
        {
            bool hasChanged = false;
            var rigGroup = await _context.RigGroup.FirstOrDefaultAsync(rg => rg.RigGroupId == updateRigGroup.RigGroupId);
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (updateRigGroup.RigGroupName != null)
            {
                rigGroup.RigGroupName = updateRigGroup.RigGroupName;
                hasChanged = true;
            }
            if (updateRigGroup.RigGroupDescription != null)
            {
                rigGroup.RigGroupDescription = updateRigGroup.RigGroupDescription;
                hasChanged = true;
            }
            if (updateRigGroup.RigIds != null)
            {
                var rigs = _context.Rig.Where(r => r.UserId == userId && updateRigGroup.RigIds.Contains(r.RigId)).ToList();
                rigGroup.Rigs = rigs;
                hasChanged = true;
            }

            if (hasChanged)
            {
                await _context.SaveChangesAsync();
            }
            return Ok(rigGroup);
        }

        [HttpGet("rigId")]
        public ActionResult<RigGroup> GetRigGroups(string rigId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var rigGroup = _context.RigGroup.Where(rg => rg.Rigs.Contains(_context.Rig.FirstOrDefault(r => r.RigId == rigId)));

            return Ok(rigGroup);
        }
    }
}
