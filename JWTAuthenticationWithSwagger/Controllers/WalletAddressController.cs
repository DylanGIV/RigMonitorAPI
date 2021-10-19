using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RigMonitorAPI.Authentication;
using RigMonitorAPI.Entities;
using RigMonitorAPI.Models.AddWalletAddressRequest;
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
    public class WalletAddressController : Controller
    {
        private readonly ILogger<WalletAddressController> _logger;
        private readonly ApplicationDbContext _context;

        public WalletAddressController(ILogger<WalletAddressController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<WalletAddress>> PostWalletAddress([FromBody] AddWalletAddressRequestModel addWalletAddress)
        {
            var walletAddress = new WalletAddress();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            walletAddress.Address = addWalletAddress.Address;
            walletAddress.PoolId = addWalletAddress.PoolId;
            walletAddress.PoolName = addWalletAddress.PoolName;
            walletAddress.UserId = userId;

            _context.WalletAddress.Add(walletAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWalletAddresses), walletAddress);
        }

        [HttpGet]
        public ActionResult<Rig> GetWalletAddresses()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var walletAddresses = _context.WalletAddress.Where(wa => wa.UserId == userId);

            return Ok(walletAddresses);
        }
    }
}
