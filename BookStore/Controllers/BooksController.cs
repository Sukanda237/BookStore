using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public BooksController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
        }

        // GET: Books
        public IActionResult Index()
        {
            var bookList = _context.Books
                .Include(c => c.Category)
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .ToList();
            IList<BookViewModel> items = new List<BookViewModel>();
            
            foreach (Book book in bookList)
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
                var booksAuthorsList = book.BookAuthors;

                foreach (BookAuthor booksAuthors in booksAuthorsList)
                {
                    var author = booksAuthors.Author;
                    authorNameList = authorNameList + author.Name + ", ";
                }
                //item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);
                items.Add(item);
            }
            return View(items);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var book = _context.Books
                .Include(c => c.Category)
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author).SingleOrDefault(b => b.BookID.Equals(id));


            var book1 = await _context.Books
                .Include(c => c.Category)
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author).FirstAsync(b => b.BookID.Equals(id));



            if (book == null)
                return RedirectToAction(nameof(Index));

            BookViewModel item = new BookViewModel();


            item.ISBN = book.BookID;
            item.Title = book.Title;
            item.PublishDate = book.PublisDate;
            item.Price = book.Price;
            item.Quantity = book.Quantity;
            //harus dicek dulu book.category nya null atau tidk
            item.CategoryName = book.Category.Name;

            string authorNameList = string.Empty;
            var booksAuthorsList = book.BookAuthors;
            foreach(BookAuthor bookAuthors in booksAuthorsList)
            {
                var author = bookAuthors.Author;
                authorNameList = authorNameList + author.Name + ", ";
            }
            //item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

            return View(item);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryID", "Name");
            ViewBag.Author = new SelectList(_context.Authors.ToList(), "AuthorID", "Name");

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookFormViewModel item)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book();

                book.BookID = item.ISBN;
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublisDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;
                _context.Add(book);
            
                if(item.AuthorIDs != null)
                {
                    foreach (int authorId in item.AuthorIDs)
                    {
                        BookAuthor bookAuthor = new BookAuthor();
                        bookAuthor.BookID = item.ISBN;
                        bookAuthor.AuthorID = authorId;
                        _context.Add(bookAuthor);
                    }
                }
            
            _context.SaveChanges();

            if (item.Photo != null)
            {
                var file = item.Photo;
                var uploads = Path.Combine(_environment.WebRootPath, "upload");
                if (file.Length > 0)
                {
                   using (var fileStream = new FileStream(Path.Combine(
                   uploads, item.ISBN + ".jpg"), FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }
                }
            }
            return RedirectToAction("Index");
        }
            return View();
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryID", "Name");
            ViewBag.Author = new SelectList(_context.Authors.ToList(), "AuthorID", "Name");

            var book = _context.Books.SingleOrDefault(p => p.BookID.Equals(id));

            BookFormViewModel item = new BookFormViewModel();
            item.ISBN = book.BookID;
            item.Title = book.Title;
            item.PublishDate = book.PublisDate;
            item.Price = book.Price;
            item.Quantity = book.Quantity;
            item.CategoryID = book.CategoryID;

            var authorList = _context.BookAuthors.Where(p => p.BookID.Equals(book.BookID)).ToList();
            List<int> authors = new List<int>();
            foreach (BookAuthor bookAuthor in authorList)
            {
                authors.Add(bookAuthor.AuthorID);
            }
            item.AuthorIDs = authors.ToArray();

            return View(item);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ISBN, CategoryID, Title, Photo, PublishDate, Price, Quantity, AuthorIDs")] BookFormViewModel item)
        {

            if (ModelState.IsValid)
            {
                _context.BookAuthors.RemoveRange(_context.BookAuthors.Where(p => p.BookID.Equals(item.ISBN)));
                _context.SaveChanges();
                Book book = _context.Books.SingleOrDefault(p => p.BookID.Equals(item.ISBN));
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublisDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;
                _context.Update(book);
                foreach (int authorId in item.AuthorIDs)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.BookID = item.ISBN;
                    bookAuthor.AuthorID = authorId;
                    _context.Add(bookAuthor);
                }
                _context.SaveChanges();
                if (item.Photo != null)
                {
                    
                    var file = item.Photo;
                    var uploads = Path.Combine(_environment.WebRootPath, "upload");
                if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(
                       uploads, item.ISBN + ".jpg"), FileMode.Create))
                        {
                            file.CopyToAsync(fileStream);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            var book = _context.Books
            .Include(c => c.Category)
            .Include(ba => ba.BookAuthors)
            .ThenInclude(a => a.Author).SingleOrDefault(b => b.BookID.Equals(id));

            BookViewModel item = new BookViewModel();
            item.ISBN = book.BookID;
            item.Title = book.Title;
            item.PublishDate = book.PublisDate;
            item.Price = book.Price;
            item.Quantity = book.Quantity;
            item.CategoryName = book.Category.Name;
            string authorNameList = string.Empty;
            var booksAuthorsList = book.BookAuthors;

            foreach (BookAuthor booksAuthors in booksAuthorsList)
            {
                var author = booksAuthors.Author;
                authorNameList = authorNameList + author.Name + ", ";
            }
            //item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

            return View(item);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                _context.BookAuthors.RemoveRange(_context.BookAuthors.Where(p => p.BookID.Equals(id)));
                _context.SaveChanges();
                var book = _context.Books.SingleOrDefault(m => m.BookID == id);
                _context.Books.Remove(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
