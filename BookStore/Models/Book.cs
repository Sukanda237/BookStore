using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public partial class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="ISBN")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [RegularExpression("^[0-9]*$", ErrorMessage =" {0} Harus Angka")]
        [StringLength(13, MinimumLength = 10, ErrorMessage =" {0} Tidak Boleh Lebih {1} dan Tidak Kurang {2} Karakter")]
        public int BookID { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        [RegularExpression("^[0-9]*$", ErrorMessage = " {0} Harus Angka")]
        public int CategoryID { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} Harus Diisi")]
        public string Title { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Display(Name = "publish Date")]
        public DateTime PublisDate { get; set; }

        [Display(Name = "Price")]
        [RegularExpression("^[0-9]*$", ErrorMessage = " {0} Harus Angka")]
        public double Price { get; set; }

        [Display(Name = "Quantity")]
        [RegularExpression("^[0-9]*$", ErrorMessage = " {0} Harus Angka")]
        public int Quantity { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
