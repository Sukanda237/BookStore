using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class Author
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "{0} Harus Diisi")]

        public int AuthorID { get; set; }

        [Display(Name = "Author's Name")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(256, ErrorMessage ="{0} Tidak Boleh Lebih dari {1} Karakter")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(256, ErrorMessage = "{0} Tidak Boleh Lebih dari {1} Karakter")]
        //[RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([azA-Z0-9]{2,4})+$", ErrorMessage =" {0} Tidak Valid.")]
        public string Email { get; set; }

        
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
