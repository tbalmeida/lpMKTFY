using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MKTFY.App
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
    }
}
