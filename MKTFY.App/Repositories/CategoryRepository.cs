using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<CategoryVM> Create(CategoryCreateVM src)
        {
            var currEntity = await _context.Categories.FirstOrDefaultAsync(cat => cat.Title == src.Title);
            try
            {
                if (currEntity != null)
                {
                    throw new Exception("Category already exists");
                }
                var entity = new Category(src);

                await _context.Categories.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new CategoryVM(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating Category.\n" + ex.Message);
            }
        }

        public async Task<List<CategoryVM>> GetAll()
        {
            var results = await _context.Categories.ToListAsync();

            var models = new List<CategoryVM>();
            foreach (var entity in results)
            {
                models.Add(new CategoryVM(entity));
            }

            return models;
        }

        public async Task<CategoryVM> GetById(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);
            if (result == null)
            {
                throw new Exception("Category not found");
            }

            return new CategoryVM(result);
        }

        public async Task<CategoryVM> Update(int id, CategoryUpdateVM src)
        {
            var currEntity = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);
            if (currEntity == null)
            {
                throw new Exception("Category not found");
            }

            currEntity.Title = src.Title;

            await _context.SaveChangesAsync();

            return new CategoryVM(currEntity);
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                var result = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == Id);
                if (result == null)
                {
                    return "Category not found";
                }
                _context.Categories.Remove(result);
                await _context.SaveChangesAsync();
                return "Category deleted";
            }
            catch
            {
                throw new Exception("Error when deleting Category");
            }
        }
    }
}
