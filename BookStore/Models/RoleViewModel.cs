using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class RoleViewModel
    {

        [Display(Name=" Role ID")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        public string RoleID { get; set; }

        [Display(Name = " Role Name")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        public string RoleName { get; set; }

        [Display(Name = " Description")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        public string Description{ get; set; }
    }
}
