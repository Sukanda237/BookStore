using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class UserCreateFormViewModel
    {
        [Display(Name="Username")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        public string[] RoleID { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(256, ErrorMessage ="{0} tidak boleh lebih dari {1} karekter")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([azA-Z0-9]{2,4})+$", ErrorMessage = "{0} tidak valid.")]        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih dari {1} karekter")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password Confirm")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih dari {1} karekter")]
        [Compare("Password", ErrorMessage = "{0} dan {1} harus sama")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih dari {1} karekter")]        public string FullName { get; set; }
    }
}
