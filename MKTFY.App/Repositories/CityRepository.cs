using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<CityVM>> GetAll()
        {
            var results = await _context.Cities.OrderBy(city=> city.Name).ToListAsync();

            var models = new List<CityVM>();
            foreach (var entity in results)
            {
                models.Add(new CityVM(entity));
            }

            return models;
        }
    }
}
