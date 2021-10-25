using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RigMonitorAPI.Authentication;
using RigMonitorAPI.Entities;
using RigMonitorAPI.Models.AddDeviceRequest;
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
    public class DeviceController : Controller
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly ApplicationDbContext _context;

        public DeviceController(ILogger<DeviceController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Device>> PostDevices([FromBody] AddDeviceRequestModel addDeviceRequest)
        {
            var devices = new List<Device>();

            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            foreach (var device in addDeviceRequest.Devices)
            {
                var matchingDevice = _context.Device.Where(d => d.RigId == device.RigId && d.Rig.UserId == userId && d.DeviceId == device.DeviceId);

                if (!matchingDevice.Any())
                {
                    var newDevice = new Device
                    {
                        DeviceId = device.DeviceId,
                        DeviceName = device.DeviceName,
                        DeviceDescription = device.DeviceDescription,
                        RigId = device.RigId
                    };

                    devices.Add(newDevice);
                }
            }

            _context.Device.AddRange(devices);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevices), devices);
        }

        [HttpGet("{rigId}")]
        public ActionResult<Device> GetDevices(string rigId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var devices = _context.Device.Where(d => d.RigId == rigId && d.Rig.UserId == userId);

            return Ok(devices);
        }
    }
}
