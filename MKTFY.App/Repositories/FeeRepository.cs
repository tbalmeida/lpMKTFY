using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class FeeRepository : IFeeRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly string _notFoundMsg = "Fee not found, please check the Id provided";

        public FeeRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<FeeVM> Create(FeeCreateVM src)
        {
            var currEntity = await _context.Fees.FirstOrDefaultAsync(f => f.Title == src.Title);

            if (currEntity != null)
                throw new Exception("Fee already exists.");

            var newEntity = new Fee(src);
            await _context.Fees.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return new FeeVM(newEntity);
        }

        public async Task<List<FeeVM>> GetAll(bool onlyActive = true)
        {
            var results = onlyActive == false ? 
                  await _context.Fees.ToListAsync() 
                : await  _context.Fees.Where(f => f.IsActive == true).ToListAsync();

            return results.ConvertAll(item => new FeeVM(item));
        }

        public Task<FeeVM> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FeeVM> Update(FeeUpdateVM src)
        {
            throw new NotImplementedException();
        }

        public Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
