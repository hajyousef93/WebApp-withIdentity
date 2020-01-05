using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication15.Data;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext context;

        public RoleController(ApplicationDbContext context)
        {
            this.context = context;
        }
       
        // GET: Role
        public ActionResult Index()
        {
            return View(context.Roles);
        }

        // GET: Role/Details/5
        public ActionResult Details(string id)
        {
            var role = context.Roles.Find(id);
            if (role == null) {
                return NotFound();
            }
            return View(role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole identityRole)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    context.Roles.Add(identityRole);
                    context.SaveChanges();
                }
               // return View(identityRole);
                // TODO: Add insert logic here
                  return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(string id)
        {
            var role = context.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,IdentityRole identityRole)
        {
            if (id != identityRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IdentityRole thisRole = context.Roles.Find(id);
                    thisRole.Name = identityRole.Name;
                    context.SaveChanges();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            return View(identityRole);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(string id)
        {
            var role = context.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
           
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(IdentityRole identityRole)
        {
            try
            {
                IdentityRole oldRole = context.Roles.Find(identityRole.Id);
                context.Roles.Remove(oldRole);
                context.SaveChanges();
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}