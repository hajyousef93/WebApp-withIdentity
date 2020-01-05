using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication15.Models;
using WebApplication15.ViewsModels;

namespace WebApplication15.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       public DbSet<Category> categories { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<WebApplication15.ViewsModels.RoleViewsModels> RoleViewsModels { get; set; }
    }
}
