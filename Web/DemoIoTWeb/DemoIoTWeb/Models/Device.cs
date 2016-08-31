using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Models
{
    public class Device
    {
        public long Id { get; set; }

        [Required]
        public String SerialNumber { get; set; }

        [Required]
        public String Description { get; set; }

        public virtual ICollection<DeviceUpdate> Updates { get; set; }

    }
}