using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> db;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.db = roleManager;
        }

        // GET: Roles
        public IActionResult Index()
        {
            var items = new List<RoleViewModel>();
            items = db.Roles.Select(r => new RoleViewModel
            {
                RoleID = r.Id,
                RoleName = r.Name,
                Description = r.Description
            }).ToList();

            return View(items);
        }


        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            RoleViewModel item = new RoleViewModel();
            ApplicationRole role = await db.FindByIdAsync(id);
            if (role != null)
            {
                item.RoleID = role.Id;
                item.RoleName = role.Name;
                item.Description = role.Description;
            }

            return View(item);
        }

        // GET: Roles/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel item)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = new ApplicationRole();
                role.Id = item.RoleID;
                role.Id = item.RoleName;
                role.Description = item.Description;

                var result = await db.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Roles/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            RoleViewModel item = new RoleViewModel();
            ApplicationRole role = await db.FindByIdAsync(id);

            if (role != null)
            {
                item.RoleID = role.Id;
                item.RoleName = role.Name;
                item.Description = role.Description;
            }
            return View(item);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("RoleID,RoleName,Description")] RoleViewModel item)
        {
            
            if(ModelState.IsValid)
            {
                ApplicationRole role = await db.FindByIdAsync(item.RoleID);
                if(role != null)
                {
                    role.Id = item.RoleID;
                    role.Name = item.RoleName;
                    role.Description = item.Description;
                    var result = await db.UpdateAsync(role);
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Roles/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            RoleViewModel item = new RoleViewModel();
            ApplicationRole role = await db.FindByIdAsync(id);
            if(role !=null)
            {
                item.RoleID = role.Id;
                item.RoleName = role.Name;
                item.Description = role.Description;
            }

            return View(item);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
              if(ModelState.IsValid)
            {
                ApplicationRole role = await db.FindByIdAsync(id);
                var result = await db.DeleteAsync(role);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
