using DemoIoTWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Areas.Admin.ViewModel
{
    public class VMDevice
    {
        public long Id { get; set; }

        public List<User> Users { get; set; }

        [Display(Name = "User")]
        [Required]
        public long SelectedUserId { get; set; }

        [Display(Name = "Serial Number")]
        [Required]
        public String SerialNumber { get; set; }

        [Display(Name = "Description")]
        [Required]
        public String Description { get; set; }
    }
}