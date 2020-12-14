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

        public DbSet<FAQ> FAQs { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ListingStatus> ListingStatuses { get; set; }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<ListingImage> ListingImages { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
