using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Models
{
    public class User
    {
        public long Id { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        public String AuthId { get; set; }

        public String APIToken { get; set; }

        public Boolean isAdmin { get; set; }

        public virtual IEnumerable<Device> Devices { get; set; }
    }
}