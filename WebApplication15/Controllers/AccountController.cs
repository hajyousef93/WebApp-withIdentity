using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication15.Data;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            
            this.userManager = userManager;
        }
        // GET: Account
        public ActionResult Index()
        {
            var user = userManager.Users.ToList();
            return View(user);
        }

        // GET: Account/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        // GET: Account/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete(string id, IFormCollection collection)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
               
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }
    }
}