﻿using IdentityServer4.Extensions;
using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{

    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        // Error message
        private readonly string _notFoundMsg = "Listing not found, please check the Id provided";

        public ListingRepository(ApplicationDbContext dbContext, IUserRepository userRepository)
        {
            _context = dbContext;
            _userRepository = userRepository;
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

        public async Task<List<ListingVM>> GetListings(int cityId, [Optional] string searchText, [Optional] int categoryId, [Optional] int itemConditionId, [Optional] int listingStatusId, [Optional] bool? activeOnly, [Optional] string ownerId)
        {
            var results = await FilterListings(cityId, searchText, categoryId, itemConditionId, listingStatusId, activeOnly, ownerId);

            var models = new List<ListingVM>();
            foreach (var item in results)
            {
                var qty = await _userRepository.ListingCount(item.UserId, true);
                models.Add(new ListingVM(item, qty));
            }

            return models;
        }

        public async Task<List<ListingShortVM>> GetListingsShort(int cityId, [Optional] string searchText, [Optional] int categoryId, [Optional] int itemConditionId, [Optional] int listingStatusId, [Optional] bool? activeOnly, [Optional] string ownerId)
        {
            var results = await FilterListings(cityId, searchText, categoryId, itemConditionId, listingStatusId, activeOnly, ownerId);

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
                .Include(item => item.City.Province)
                .FirstOrDefaultAsync(lst => lst.Id == id);

            if (result == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            var qty = await _userRepository.ListingCount(result.UserId, true);

            return new ListingVM(result, qty);
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

        public async Task<List<Listing>> FilterListings([Optional] int? cityId, [Optional] string? searchText, [Optional] int? categoryId, [Optional] int? itemConditionId, [Optional] int? listingStatusId, [Optional] bool? activeOnly, [Optional] string? ownerId)
        {

            var query = _context.Listings
                .Include(item => item.Category)
                .Include(item => item.ItemCondition)
                .Include(item => item.ListingStatus)
                .Include(item => item.City)
                .Include(item => item.User)
                .Include(item => item.City.Province)
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
            if (!ownerId.IsNullOrEmpty())
                query = query.Where(item => item.UserId == ownerId);

            // filter by City
            if (cityId > 0)
                query = query.Where(item => item.CityId == cityId);

            // filter by Listing Status
            if (listingStatusId > 0)
                query = query.Where(item => item.ListingStatusId == listingStatusId);

            // filter by Category
            if (categoryId > 0)
                query = query.Where(item => item.CategoryId == categoryId);

            // filter by Item Condition
            if (itemConditionId > 0)
                query = query.Where(item => item.ItemConditionId == itemConditionId);

            // filter only active statuses
            if (activeOnly == true | activeOnly == false)
                query = query.Where(item => item.ListingStatus.IsActive == activeOnly);

            var results = await query.OrderBy(lst => lst.Created).ToListAsync();
            return results;
        }
    }
}
