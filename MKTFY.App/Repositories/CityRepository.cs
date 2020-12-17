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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<CityVM> Create(CityCreateVM src)
        {
            try
            {
                var entity = new City(src);

                await _context.Cities.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new CityVM(entity);
            }
            catch
            {
                throw new Exception("Error on City creation.");
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                var result = await _context.Cities.FirstOrDefaultAsync(f => f.Id == id);
                if (result == null)
                {
                    return "City not found";
                }
                _context.Cities.Remove(result);
                await _context.SaveChangesAsync();
                return "City deleted";
            }
            catch
            {
                throw new Exception("Error when deleting FAQ");
            }
        }

        public async Task<List<CityVM>> GetAll()
        {
            var results = await _context.Cities.OrderBy(city=> city.Name).ToListAsync();

            var models = new List<CityVM>();
            foreach (var entity in results)
            {
                models.Add(new CityVM(entity));
            }

            return models;
        }

        public async Task<CityVM> GetById(int id)
        {
            var result = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                throw new Exception("City not found");
            }

            return new CityVM(result);
        }

        public async Task<CityVM> Update(int id, CityUpdateVM src)
        {
            if (id != src.Id)
            {
                throw new Exception("Invalid data");
            }

            var currentEntity = await _context.Cities.FirstOrDefaultAsync(city => city.Id == id);
            if (currentEntity == null)
            {
                throw new Exception("City not found");
            }

            currentEntity.Name = src.Name;
            currentEntity.ProvinceId = src.ProvinceId;

            await _context.SaveChangesAsync();

            return new CityVM(currentEntity);
        }
    }
}
