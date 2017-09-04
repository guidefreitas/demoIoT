using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.ViewModel
{
    public class VMLogin
    {
        [Display(Name = "Email")]
        [Required]
        public String Email { get; set; }

        [Display(Name = "Senha")]
        [Required]
        public String Password { get; set; }
    }
}