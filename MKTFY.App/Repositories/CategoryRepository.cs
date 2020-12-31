using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly string _notFoundMsg = "Category not found, please check the Id provided";

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
                throw new Exception("Error while creating Category. " + ex.Message);
            }
        }

        public async Task<List<CategoryVM>> GetAll()
        {
            try
            {
                var results = await _context.Categories.ToListAsync();

                var models = new List<CategoryVM>();
                foreach (var entity in results)
                {
                    models.Add(new CategoryVM(entity));
                }

                return models;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Categories. " + ex.Message);
            }
        }

        public async Task<CategoryVM> GetById(int id)
        {
            try
            {
                var result = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);
                if (result == null)
                    throw new NotFoundException( _notFoundMsg, id.ToString() );

                return new CategoryVM(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving a Category. " + ex.Message);
            }
        }

        public async Task<CategoryVM> Update(int id, CategoryUpdateVM src)
        {
            try
            {
                var currEntity = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);
                if (currEntity == null)
                    throw new NotFoundException(_notFoundMsg, id.ToString());

                currEntity.Title = src.Title;

                await _context.SaveChangesAsync();

                return new CategoryVM(currEntity);
            }
            catch (Exception ex)
            {
                throw new NotFoundException( _notFoundMsg, id.ToString() );
            }
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                var result = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == Id);
                if (result == null)
                    throw new NotFoundException(_notFoundMsg, Id.ToString());

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
