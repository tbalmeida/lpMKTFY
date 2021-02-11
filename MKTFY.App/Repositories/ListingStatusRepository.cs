using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System;
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
            try
            {
                var results = await _context.ListingStatuses.ToListAsync();

                var models = new List<ListingStatusVM>();
                foreach (var item in results)
                {
                    models.Add(new ListingStatusVM(item));
                }

                return models;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Listing Statuses. " + ex.Message);
            }
        }

        // Returns the Id for a Status, searching by name. If not found, return -1 and treat the erro on the caller
        public async Task<int> GetByName(string name)
        {
            var result = await _context.ListingStatuses.FirstOrDefaultAsync(i => i.Name == name);
            return result == null ? -1 : result.Id;                
        }

        // Checks if a Status is active (by default, the Active or Pending are active); if not found, returns False 
        public async Task<bool> IsActive(int id)
        {
            var result = await _context.ListingStatuses.FindAsync(id);
            return result != null && result.IsActive;
        }
    }
}
