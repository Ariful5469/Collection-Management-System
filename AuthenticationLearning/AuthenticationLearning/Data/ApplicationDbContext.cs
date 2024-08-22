using AuthenticationLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationLearning.Data
{ 
    public class ApplicationDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SalesLeadEntity> SalesLead { get; set; }

    }
}
