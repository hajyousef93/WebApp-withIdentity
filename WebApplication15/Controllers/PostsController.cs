using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication15.Data;
using WebApplication15.Helper;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private IHostingEnvironment _environment;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(ApplicationDbContext context, IHostingEnvironment environment,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            this.userManager = userManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.posts.Include(p => p.Category).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //connection Database and Excute stored procedure
            var con = _context.Database.GetDbConnection();
            con.Open();
            var comm = con.CreateCommand();
            comm.CommandText = "IncReader";
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("Id", id));
            comm.ExecuteNonQuery();


            var post = await _context.posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Subject,Body,PublicationDate,AuthorName,UrlImage")] Post post, IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                post.UrlImage = await UserFile.UploadeNewImageAsync(post.UrlImage, myfile, _environment.WebRootPath, Properties.Resources.ImgFolder, 100, 100);

                post.UserId = userManager.GetUserId(User);

                post.PublicationDate = System.DateTime.Today.Date; 
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", post.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", post.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Subject,Body,PublicationDate,AuthorName,UrlImage")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", post.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.posts.FindAsync(id);
            _context.posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.posts.Any(e => e.Id == id);
        }
    }
}
