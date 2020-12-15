using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.App.Seeds
{
    public static class BasicDataSeeder
    {
        public static async Task SeedBasicConfig(ApplicationDbContext dbContext)
        {
            var country = await dbContext.Countries.Where(c => c.Name == "Canada").FirstOrDefaultAsync();
            if (country == null)
            {
                country = new Country { Name = "Canada", Abbreviation = "CA" };
                await dbContext.Countries.AddAsync(country);
                await dbContext.SaveChangesAsync();

                var province = new Province { Name = "Alberta", Abbreviation = "AB", CountryId = country.Id };
                await dbContext.Provinces.AddAsync(province);
                await dbContext.SaveChangesAsync();
                var city = new List<City>
                {
                    new City { Name = "Calgary", ProvinceId = province.Id},
                    new City { Name = "Airdrie", ProvinceId = province.Id},
                    new City { Name = "Cochrane", ProvinceId = province.Id},
                    new City { Name = "Chestermere", ProvinceId = province.Id},
                    new City { Name = "Okotoks", ProvinceId = province.Id}
                };

                await dbContext.AddRangeAsync(city);
                await dbContext.SaveChangesAsync();

                var listStatuses = new List<ListingStatus>
                {
                    new ListingStatus { Name = "Active"},
                    new ListingStatus { Name = "Pending"},
                    new ListingStatus { Name = "Completed"},
                    new ListingStatus { Name = "Cancelled"},
                    new ListingStatus { Name = "Archived"}
                };

                await dbContext.AddRangeAsync(listStatuses);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
