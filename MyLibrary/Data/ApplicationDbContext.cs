using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Models; // Ensure you include your models namespace

namespace MyLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Use IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        // Define DbSets for your custom models here

    }
}
