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

        public async Task<int> GetStatusId(string name)
        {
            var result = await _context.ListingStatuses.FirstOrDefaultAsync(item => item.Name == name);

            if (result == null)
                return -1;

            return result.Id;
        }
    }
}
