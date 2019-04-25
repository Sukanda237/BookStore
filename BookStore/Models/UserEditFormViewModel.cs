using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class UserEditFormViewModel
    {
        [Display(Name="Username")]
        [Required(ErrorMessage = "{0} Harus diisi")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} Harus diisi")]
        public string[] RoleID { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} Harus diisi")]
        [StringLength(256,ErrorMessage = "{0} tidak lebih dari {1} karakter")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([azA-Z0-9]{2,4})+$", ErrorMessage = "{0} tidak valid.")]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "{0} Harus diisi")]
        [StringLength(256, ErrorMessage = "{0} tidak lebih dari {1} karakter")]
        public string FullName { get; set; }
    }
}
