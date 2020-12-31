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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        // Error message
        private readonly string _notFoundMsg = "City not found, please check the Id provided";

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
                var result = await _context.Cities.FirstOrDefaultAsync(f => f.Id == id);
                if (result == null)
                    throw new NotFoundException(_notFoundMsg, id.ToString());

                _context.Cities.Remove(result);
                await _context.SaveChangesAsync();
                return "City deleted";
        }

        public async Task<List<CityVM>> GetAll()
        {
            try
            {
                var results = await _context.Cities.OrderBy(city=> city.Name).ToListAsync();

                var models = new List<CityVM>();
                foreach (var entity in results)
                {
                    models.Add(new CityVM(entity));
                }

                return models;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Cities.");
            }
        }

        public async Task<CityVM> GetById(int id)
        {
            var result = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            return new CityVM(result);
        }

        public async Task<CityVM> Update(int id, CityUpdateVM src)
        {
            if (id != src.Id)
                throw new MismatchingId(id.ToString());

            var currentEntity = await _context.Cities.FirstOrDefaultAsync(city => city.Id == id);
            if (currentEntity == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            currentEntity.Name = src.Name;
            currentEntity.ProvinceId = src.ProvinceId;

            await _context.SaveChangesAsync();

            return new CityVM(currentEntity);
        }
    }
}
