using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Models
{
    public class DeviceUpdate
    {
        public long Id { get; set; }

        public String Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}