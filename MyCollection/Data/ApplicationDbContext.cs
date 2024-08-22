using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCollection.Models;

namespace MyCollection.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } // Ensure this DbSet is defined
        public DbSet<User> Users { get; set; } // Example for User
        public DbSet<Comment> Comments { get; set; } // Example for Comment
    }
}
