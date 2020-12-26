using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class ItemConditionRepository : IItemConditionRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemConditionRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<ItemConditionVM>> GetAll()
        {
            var results = await _context.ItemConditions.ToListAsync();

            var models = new List<ItemConditionVM>();
            foreach (var item in results)
            {
                models.Add(new ItemConditionVM(item));
            }

            return models;
        }
    }
}
