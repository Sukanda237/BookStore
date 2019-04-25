using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class UserViewModel
    {

        [Display(Name="Username")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get; set; }
    }
}
