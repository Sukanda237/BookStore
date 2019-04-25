using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BookController_versi1 : Controller
    {

        private readonly ApplicationDbContext db;

        public BookController_versi1(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var booklist = db.Books
                .Include(c => c.Category)
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .ToList();

            IList<BookViewModel> items = new List<BookViewModel>();
            foreach(Book book in booklist)
            {
                BookViewModel item = new BookViewModel();

                item.ISBN = book.BookID;
                item.Title = book.Title;
                item.Photo = book.Photo;
                item.PublishDate = book.PublisDate;
                item.Price = book.Price;
                item.Quantity = book.Quantity;
                item.CategoryName = book.Category.Name;

                string authorNameList = string.Empty;
                var bookAuthorList = book.BookAuthors;
                foreach(BookAuthor bookAuthor in bookAuthorList)
                {
                    var author = bookAuthor.Author;
                    authorNameList = authorNameList + author.Name + " , ";
                }
                //item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

                items.Add(item);

            }
            return View(items);
        }
    }
}