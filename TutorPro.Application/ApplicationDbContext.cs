using Microsoft.EntityFrameworkCore;
using TutorPro.Application.Models;

namespace TutorPro.Application
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<WaitlistUsers> WaitlistUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if(!Database.CanConnect() || Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }

            WaitlistUsers = Set<WaitlistUsers>();
        }      
    }
}
