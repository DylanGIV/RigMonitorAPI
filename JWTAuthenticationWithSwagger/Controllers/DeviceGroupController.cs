using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RigMonitorAPI.Authentication;
using RigMonitorAPI.Entities;
using RigMonitorAPI.Models.DeviceGroup.AddDeviceGroupRequest;
using RigMonitorAPI.Models.DeviceGroup.UpdateDeviceGroupRequest;
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
    public class DeviceGroupController : Controller
    {
        private readonly ILogger<DeviceGroupController> _logger;
        private readonly ApplicationDbContext _context;

        public DeviceGroupController(ILogger<DeviceGroupController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<DeviceGroup>> PostDeviceGroup([FromBody] AddDeviceGroupRequestModel addDeviceGroup)
        {
            var deviceGroup = new DeviceGroup();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var devices = _context.Device.Where(d => d.Rig.UserId == userId).ToList();

            deviceGroup.DeviceGroupName = addDeviceGroup.DeviceGroupName;
            deviceGroup.DeviceGroupDescription = addDeviceGroup.DeviceGroupDescription;

            _context.DeviceGroup.Add(deviceGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeviceGroups), deviceGroup);
        }

        [HttpPut]
        public async Task<ActionResult<DeviceGroup>> PutDeviceGroup([FromBody] UpdateDeviceGroupRequestModel updateDeviceGroup)
        {
            bool hasChanged = false;
            var deviceGroup = await _context.DeviceGroup.FirstOrDefaultAsync(dg => dg.DeviceGroupId == updateDeviceGroup.DeviceGroupId);
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (updateDeviceGroup.DeviceGroupName != null)
            {
                deviceGroup.DeviceGroupName = updateDeviceGroup.DeviceGroupName;
                hasChanged = true;
            }
            if (updateDeviceGroup.DeviceGroupDescription != null)
            {
                deviceGroup.DeviceGroupDescription = updateDeviceGroup.DeviceGroupDescription;
                hasChanged = true;
            }
            if (updateDeviceGroup.DeviceIds != null)
            {
                var devices = _context.Device.Where(d => d.Rig.UserId == userId && updateDeviceGroup.DeviceIds.Contains(d.DeviceId)).ToList();
                deviceGroup.Devices = devices;
                hasChanged = true;
            }

            if (hasChanged)
            {
                await _context.SaveChangesAsync();
            }
            return Ok(deviceGroup);
        }

        [HttpGet("deviceId")]
        public ActionResult<DeviceGroup> GetDeviceGroups(string deviceId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var deviceGroup = _context.DeviceGroup.Where(dg => dg.Devices.Contains(_context.Device.FirstOrDefault(d => d.DeviceId == deviceId)));

            return Ok(deviceGroup);
        }
    }
}
