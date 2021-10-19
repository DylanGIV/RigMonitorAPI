using RigMonitorAPI.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RigMonitorAPI.Entities
{
    public class DeviceStats
    {
        public DateTime Timestamp { get; set; }
        public short Temperature { get; set; }

        [Column(TypeName = "decimal(3, 1)")]
        public decimal PowerUsage { get; set; }
        public short FanSpeed { get; set; }
        public short MemoryClockSpeed { get; set; }
        public short CoreClockSpeed { get; set; }
        public short DeviceUsage { get; set; }
        public string DeviceId { get; set; }
        public virtual Device Device { get; set; }
        
    }
}
