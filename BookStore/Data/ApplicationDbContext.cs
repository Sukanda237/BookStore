using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Author> Authors{ get; set; }
        public virtual DbSet<Book> Books{ get; set; }
        public virtual DbSet<BookAuthor> BookAuthors{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
