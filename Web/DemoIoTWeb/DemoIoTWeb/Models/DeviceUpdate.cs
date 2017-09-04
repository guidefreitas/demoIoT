using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Models
{
    public class DeviceUpdate
    {
        public DeviceUpdate()
        {
            this.DateTime = DateTime.MinValue;
        }

        public long Id { get; set; }

        public String Value { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public virtual Device Device { get; set; }
    }
}