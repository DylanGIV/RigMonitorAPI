using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RigMonitorAPI.Authentication;
using RigMonitorAPI.Entities;
using RigMonitorAPI.Models.Request;
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
    public class RigController : Controller
    {
        private readonly ILogger<RigController> _logger;
        private readonly ApplicationDbContext _context;

        public RigController(ILogger<RigController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Rig>> PostRig([FromBody] AddRigRequestModel addRigRequest)
        {
            var rig = new Rig();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            rig.UserId = userId;
            rig.RigName = addRigRequest.RigName;
            rig.RigDescription = addRigRequest.RigDescription;

            _context.Rig.Add(rig);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRig), rig);
        }

        [HttpGet]   
        public ActionResult<Rig> GetRig()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var rigs = _context.Rig.Where(r => r.UserId == userId);

            return Ok(rigs);
        }

        [HttpGet("rigId")]
        public ActionResult<Rig> GetRig(long rigId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var rig = _context.Rig.FirstOrDefault(r => r.UserId == userId && r.RigId == rigId);

            return Ok(rig);
        }
    }
}
