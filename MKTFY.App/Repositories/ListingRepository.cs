using IdentityServer4.Extensions;
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
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        // Error message
        private readonly string _notFoundMsg = "Listing not found, please check the Id provided";

        public ListingRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<ListingVM> Create(ListingCreateVM src)
        {
            try
            {
                var entity = new Listing(src);

                await _context.Listings.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ListingVM(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error on Listing creation. " + ex.Message);
            }
        }

        public async Task<string> Delete(Guid id)
        {
            try
            {
                var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
                if (result == null)
                    throw new NotFoundException( _notFoundMsg, id.ToString() );

                // TBI - add deletion of images and transactions

                _context.Listings.Remove(result);
                await _context.SaveChangesAsync();

                return "Listing deleted";
            }
            catch (Exception ex) when (ex.GetType().Name != "NotFoundException")
            {
                throw new Exception("Error when deleting Listing. " + ex.Message);
            }
        }

        public async Task<List<ListingVM>> GetListings(string searchText, int? searchCity, int? searchStatus, int? searchCategory, int? searchItemCond, string? owner)
        {
            var results = await FilterListings(searchText, searchCity, searchStatus, searchCategory, searchItemCond, owner);

            var models = new List<ListingVM>();
            foreach (var item in results)
            {
                models.Add(new ListingVM(item));
            }

            return models;
        }

        public async Task<List<ListingShortVM>> GetListingsShort(string searchText, int? searchCity, int? searchStatus, int? searchCategory, int? searchItemCond, string? owner)
        {
            var results = await FilterListings(searchText, searchCity, searchStatus, searchCategory, searchItemCond, owner);

            var models = new List<ListingShortVM>();
            foreach (var item in results)
            {
                models.Add(new ListingShortVM(item));
            }

            return models;
        }

        public async Task<ListingVM> GetById(Guid id)
        {
            var result = await _context.Listings
                .Include(item => item.Category)
                .Include(item => item.City)
                .Include(item => item.ItemCondition)
                .Include(item => item.ListingStatus)
                .Include(item => item.User)
                .FirstOrDefaultAsync(lst => lst.Id == id);

            if (result == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            return new ListingVM(result);
        }

        public async Task<ListingVM> Update(Guid id, ListingUpdateVM src)
        {
            if (id != src.Id)
                throw new MismatchingId(id.ToString());

            var curListing = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
            if (curListing == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            curListing.Title = src.Title;
            curListing.Description = src.Description;
            curListing.CategoryId = src.CategoryId;
            curListing.Price = src.Price;
            curListing.CityId = src.CityId;
            curListing.ListingStatusId = src.ListingStatusId;
            curListing.Updated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ListingVM(curListing);
        }

        public async Task<ListingVM> UpdatePrice(Guid id, ListingUpdatePriceVM src)
        {

            var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
            if (result == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            result.Price = src.Price;
            await _context.SaveChangesAsync();

            return new ListingVM(result);
        }

        public async Task<ListingVM> UpdateStatus(Guid id, ListingUpdateStatusVM src)
        {

            var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
            if (result == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            result.ListingStatusId = src.ListingStatusId;
            await _context.SaveChangesAsync();

            return new ListingVM(result);
        }

        public async Task<List<Listing>> FilterListings(string searchText, int? searchCity, int? searchStatus, int? searchCategory, int? searchItemCond, string? owner)
        {
            var query = _context.Listings
                .Include(item => item.Category)
                .Include(item => item.ItemCondition)
                .Include(item => item.ListingStatus)
                .Include(item => item.City)
                .Include(item => item.User)
                .AsQueryable();

            // Filter by text
            if (!searchText.IsNullOrEmpty())
            {
                var lowerSearchText = searchText.ToLower();
                query = query.Where(item =>
                    item.Title.ToLower().Contains(lowerSearchText)
                    || item.Description.ToLower().Contains(lowerSearchText)
                );
            }

            // Filter by owner
            if (!owner.IsNullOrEmpty())
                query = query.Where(item => item.UserId == owner);

            // filter by City
            if (searchCity.HasValue)
                query = query.Where(item => item.CityId == searchCity);

            // filter by Listing Status
            searchStatus = searchStatus.HasValue ? searchStatus : 1;
            query = query.Where(item => item.ListingStatusId == searchStatus);

            // filter by Category
            if (searchCategory.HasValue)
                query = query.Where(item => item.CategoryId == searchCategory);

            // filter by Item Condition
            if (searchItemCond.HasValue)
                query = query.Where(item => item.ItemConditionId == searchItemCond);

            var results = await query.OrderBy(lst => lst.Created).ToListAsync();
            return results;
        }
    }
}
