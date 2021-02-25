using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
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

        public async Task<FeeVM> GetById(int id)
        {
            var results = await GetFee(id);

            return new FeeVM(results);
        }

        public async Task<FeeVM> Update(FeeUpdateVM src)
        {
            var results = await GetFee(src.Id);

            results.Title = src.Title;
            results.Value = src.Value;
            results.Notes = src.Notes;
            results.IsActive = src.IsActive;
            results.IsPercentual = src.IsPercentual;
            results.Cap = src.IsPercentual ? src.Cap : 0;

            await _context.SaveChangesAsync();

            return new FeeVM(results);
        }

        public async Task<string> Delete(int id)
        {
            var result = await GetFee(id);

            _context.Fees.Remove(result);
            await _context.SaveChangesAsync();

            return "Fee deleted.";
        }

        private async Task<Fee> GetFee(int id)
        {
            var entity = await _context.Fees.FindAsync(id);

            if (entity == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            return entity;
        }
    }
}
