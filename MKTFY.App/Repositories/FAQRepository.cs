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
    public class FAQRepository : IFAQRepository
    {
        private readonly ApplicationDbContext _context;

        // Error message
        private readonly string _notFoundMsg = "FAQ not found, please check the Id provided";


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
            catch (Exception ex)
            {
                throw new Exception("Error on FAQ creation.\n" + ex.Message);
            }
        }

        public async Task<string> Delete(Guid id)
        {
            try
            {
                var result = await _context.FAQs.FirstOrDefaultAsync(f => f.Id == id);
                if (result == null)
                    throw new NotFoundException(_notFoundMsg, id.ToString());

                _context.FAQs.Remove(result);
                await _context.SaveChangesAsync();

                return "FAQ deleted";
            }
            catch
            {
                throw new Exception("Error when deleting FAQ");
            }
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
                throw new NotFoundException(_notFoundMsg, id.ToString());

            return new FAQVM(result);
        }

        public async Task<FAQVM> Update(Guid id, FAQUpdateVM src)
        {
            if (id != src.Id)
                throw new MismatchingId( id.ToString() );

            var thisFAQ = await _context.FAQs.FirstOrDefaultAsync(x => x.Id == id);
            if (thisFAQ == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            thisFAQ.Text = src.Text;
            thisFAQ.Title = src.Title;

            await _context.SaveChangesAsync();

            return new FAQVM(thisFAQ);
        }

    }
}
