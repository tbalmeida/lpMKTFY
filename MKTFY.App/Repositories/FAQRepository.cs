using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class FAQRepository : IFAQRepository
    {
        private readonly ApplicationDbContext _context;

        public FAQRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<FAQVM> Create(FAQCreateVM src)
        {
            try
            {
                var entity = new FAQ(src);

                await _context.FAQs.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new FAQVM(entity);
            }
            catch
            {
                throw new Exception("Error on FAQ creation.");
            }
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FAQVM>> FilterFAQ(string searchTerm = null)
        {
            try
            {
                var results = await _context.FAQs.Where(fq => fq.Title.ToLower().Contains(searchTerm.ToLower()) || fq.Text.ToLower().Contains(searchTerm.ToLower())).ToListAsync();

                var models = new List<FAQVM>();
                foreach (var entity in results)
                {
                    models.Add(new FAQVM(entity));
                }
                return models;
            }
            catch
            {
                throw new Exception("Error on FAQs search.");
            }
        }

        public async Task<List<FAQVM>> GetAll()
        {
            var results = await _context.FAQs.ToListAsync();

            var models = new List<FAQVM>();
            foreach (var entity in results)
            {
                models.Add(new FAQVM(entity));
            }
            return models;
        }

        public async Task<FAQVM> GetById(Guid id)
        {
            var result = await _context.FAQs.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new Exception("FAQ not found");
            }
            return new FAQVM(result);
        }

        public Task<FAQVM> Update(FAQUpdateVM src)
        {
            throw new NotImplementedException();
        }
    }
}
