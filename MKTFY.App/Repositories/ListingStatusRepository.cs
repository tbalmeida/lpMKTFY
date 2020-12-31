using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class ListingStatusRepository : IListingStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingStatusRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<ListingStatusVM>> GetAll()
        {
            var results = await _context.ListingStatuses.ToListAsync();

            var models = new List<ListingStatusVM>();
            foreach (var item in results)
            {
                models.Add(new ListingStatusVM(item));
            }

            return models;
        }
    }
}
