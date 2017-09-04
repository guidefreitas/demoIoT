using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Areas.Admin.ViewModel
{
    public class VMUser
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Email")]
        [Required]
        public String Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        public String Password { get; set; }

        [Display(Name = "Password Confirmation")]
        [Required]
        public String PasswordConfirmation { get; set; }

        [Display(Name = "Is Administrator")]
        public Boolean isAdmin { get; set; }
    }
}