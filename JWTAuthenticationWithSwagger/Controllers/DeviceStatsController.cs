using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RigMonitorAPI.Authentication;
using RigMonitorAPI.Entities;
using RigMonitorAPI.Models;
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
    public class DeviceStatsController : ControllerBase
    {
        private readonly ILogger<DeviceStatsController> _logger;
        private readonly ApplicationDbContext _context;

        public DeviceStatsController(ILogger<DeviceStatsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<DeviceStats>> PostDeviceStats([FromBody]AddDeviceStatsRequestModel addDeviceStatsRequest)
        {
            var devicesStats = new List<DeviceStats>();

            foreach (var deviceStats in addDeviceStatsRequest.DeviceStats)
            {
                var newDeviceStats = new DeviceStats
                {
                    Timestamp = addDeviceStatsRequest.Timestamp,
                    Temperature = deviceStats.Temperature,
                    PowerUsage = deviceStats.PowerUsage,
                    FanSpeed = deviceStats.FanSpeed,
                    MemoryClockSpeed = deviceStats.MemoryClockSpeed,
                    CoreClockSpeed = deviceStats.CoreClockSpeed,
                    DeviceUsage = deviceStats.DeviceUsage,
                    DeviceId = deviceStats.DeviceId
                };

                devicesStats.Add(newDeviceStats);
            }

            _context.DeviceStats.AddRange(devicesStats);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeviceStats), devicesStats);
        }

        [HttpGet("rigId")]
        public ActionResult<DeviceStats> GetDeviceStats(string rigId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var deviceStats = _context.DeviceStats.Where(ds => ds.Device.RigId == rigId && ds.Device.Rig.UserId == userId);

            return Ok(deviceStats);
        }
    }
}
