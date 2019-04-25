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
    public class AuthorController_versi1 : Controller
    {
        private readonly ApplicationDbContext db;

        public AuthorController_versi1(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
              
            return View(await db.Authors.ToListAsync());
        }

        //public async Task<IActionResult> Input()
        //{
        //    return View();
        //}
    }
}