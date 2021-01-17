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
            }

            var activeStatus = await dbContext.ListingStatuses.Where(ls => ls.Name == "Active").FirstOrDefaultAsync();
            if (activeStatus == null)
            {
                var listStatuses = new List<ListingStatus>
                {
                    new ListingStatus { Name = "Active", IsActive = true},
                    new ListingStatus { Name = "Pending", IsActive = true},
                    new ListingStatus { Name = "Completed", IsActive = false},
                    new ListingStatus { Name = "Cancelled", IsActive = false},
                    new ListingStatus { Name = "Archived", IsActive = false}
                };

                await dbContext.AddRangeAsync(listStatuses);
                await dbContext.SaveChangesAsync();
            }

            var itemUsed = await dbContext.ItemConditions.Where(ic => ic.Name == "Used").FirstOrDefaultAsync();
            if (itemUsed == null)
            {
                var itemConditions = new List<ItemCondition>
                {
                    new ItemCondition { Name = "Used"},
                    new ItemCondition { Name = "New in sealed box"},
                    new ItemCondition { Name = "New in opened box"},
                    new ItemCondition { Name = "New without box"}
                };

                await dbContext.AddRangeAsync(itemConditions);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
