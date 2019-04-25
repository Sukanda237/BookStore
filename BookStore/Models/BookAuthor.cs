using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class BookAuthor
    {
        [ForeignKey("Book")]
        [Display(Name ="ISBN")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = " {0} Tidak Boleh Lebih {1} dan Tidak Kurang {2} Karakter")]
        public int BookAuthorID { get; set; }


        public int BookID { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Author")]
        [Display(Name = "AuthorID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public int AuthorID { get; set; }

        public Author Author { get; set; }
    }
}
