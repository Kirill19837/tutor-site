using Microsoft.EntityFrameworkCore;
using TutorPro.Application.Models;

namespace TutorPro.Application
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<WaitlistUsers> WaitlistUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {          
            WaitlistUsers = Set<WaitlistUsers>();
        }      
    }
}
