using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class Category
    {

        [Display(Name = "ID")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        public int CategoryID { get; set; }

        [Display(Name= "Book Category Name")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(256, ErrorMessage = "{0} Tidak Boleh Lebih {1} Karakter")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
