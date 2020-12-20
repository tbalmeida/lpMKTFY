﻿using Microsoft.EntityFrameworkCore;
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
                throw new Exception("Error on Listing creation\n" + ex.Message);
            }
        }

        public async Task<string> Delete(Guid id)
        {
            try
            {
                var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
                if (result == null)
                {
                    return "Listing not found";
                }

                // TBI - add deletion of images and transactions

                _context.Listings.Remove(result);
                await _context.SaveChangesAsync();

                return "Listing deleted";
            }
            catch (Exception ex)
            {
                throw new Exception("Error when deleting Listing\n" + ex.Message);
            }
        }

        public async Task<List<ListingVM>> GetAll(int status = 1, string searchText = null, int? filterCategory = null, Guid? owner = null)
        {
            // TBI - filters
            var results = await _context.Listings.OrderBy(lst => lst.Created).ToListAsync();

            var models = new List<ListingVM>();
            foreach (var item in results)
            {
                models.Add(new ListingVM(item));
            }

            return models;
        }

        public async Task<ListingVM> GetById(Guid id)
        {
            var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
            if (result == null)
            {
                throw new Exception("Listing not found");
            }

            return new ListingVM(result);
        }

        public async Task<ListingVM> Update(Guid id, ListingUpdateVM src)
        {
            if (id != src.Id)
            {
                throw new Exception("Invalid data");
            }

            var curListing = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
            if (curListing == null)
            {
                throw new Exception("Listing not found");
            }

            curListing.Title = src.Title;
            curListing.Description = src.Description;
            curListing.CategoryId = src.CategoryId;
            curListing.Price = src.Price;
            curListing.CityId = src.CityId;
            curListing.ListingStatusId = src.ListingStatusId;

            await _context.SaveChangesAsync();

            return new ListingVM(curListing);
        }

        public async Task<bool> UpdatePrice(Guid id, decimal newPrice)
        {
            bool thisResult = false;

            var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
            if (result == null)
            {
                throw new Exception("Listing not found");
            }

            result.Price = newPrice;
            await _context.SaveChangesAsync();
            thisResult = true;

            return thisResult;
        }

        public async Task<bool> UpdateStatus(Guid id, int newStatus)
        {
            bool thisResult = false;

            var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);
            if (result == null)
            {
                throw new Exception("Listing not found");
            }

            result.ListingStatusId = newStatus;
            await _context.SaveChangesAsync();
            thisResult = true;

            return thisResult;
        }
    }
}