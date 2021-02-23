using IdentityServer4.Extensions;
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
        private readonly IListingStatusRepository _listingStatusRepository;
        private readonly IOrderRepository _orderRepository;

        // Error message
        private readonly string _notFoundMsg = "Listing not found, please check the Id provided";

        public ListingRepository(ApplicationDbContext dbContext, 
            IUserRepository userRepository, 
            IListingStatusRepository listingStatusRepository,
            IOrderRepository orderRepository)
        {
            _context = dbContext;
            _userRepository = userRepository;
            _listingStatusRepository = listingStatusRepository;
            _orderRepository = orderRepository;
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

        public async Task<List<ListingVM>> GetListings([Optional] int cityId, [Optional] string searchText, [Optional] int categoryId, [Optional] int itemConditionId, [Optional] int listingStatusId, [Optional] bool activeOnly, [Optional] string ownerId)
        {
            var results = await FilterListings(
                activeOnly: activeOnly, 
                cityId: cityId, 
                searchText: searchText, 
                categoryId: categoryId, 
                itemConditionId: itemConditionId, 
                listingStatusId: listingStatusId, 
                ownerId: ownerId);

            var models = new List<ListingVM>();
            foreach (var item in results)
            {
                var qty = await _userRepository.ListingCount(item.UserId, true);
                models.Add(new ListingVM(item, qty));
            }

            return models;
        }

        public async Task<List<ListingShortVM>> GetListingsShort([Optional] int cityId, [Optional] string searchText, [Optional] int categoryId, [Optional] int itemConditionId, [Optional] int listingStatusId, [Optional] bool activeOnly, [Optional] string ownerId)
        {
            var results = await FilterListings(
                activeOnly: activeOnly,
                cityId: cityId,
                searchText: searchText,
                categoryId: categoryId,
                itemConditionId: itemConditionId,
                listingStatusId: listingStatusId,
                ownerId: ownerId);

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

        public async Task<bool> UpdateStatus(Guid id, int newStatusId)
        {
            try
            {
                var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);

                result.ListingStatusId = newStatusId;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Listing>> FilterListings(int cityId = 0, string searchText = null, int categoryId = 0, int itemConditionId = 0, int listingStatusId = 0, bool activeOnly = true, string ownerId = null)
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
            if (activeOnly)
                query = query.Where(item => item.ListingStatus.IsActive == true);

            var results = await query.OrderBy(lst => lst.Created).ToListAsync();
            return results;
        }

        public async Task<OrderVM> Buy(OrderCreateVM src)
        {
            int thisStatus = await GetStatus(src.ListingId);
            int statusActive = await ValidateState("Active");

            if (thisStatus != statusActive)
                throw new Exception("This listing is not for sale.");

            var dbTrans = _context.Database.BeginTransaction();

            try
            {
                int statusPending = await _listingStatusRepository.GetByName("Pending");

                // Create an order and set it to Pending
                src.OrderStatusId = await ValidateState("Pending");

                decimal thisPrice = await GetPrice(src.ListingId);
                src.TotalPaid = await TotalPrice(thisPrice);

                var thisOrder = await _orderRepository.Create(src);

                // updates the listing status to pending
                bool listingOk = await UpdateStatus(src.ListingId, statusPending);

                if (!listingOk)
                {
                    await dbTrans.RollbackAsync();
                    throw new Exception("Could not create an order for this listing. Please, try again later.");
                }
                // commit all changes to database and return an Order view
                await dbTrans.CommitAsync();

                return new OrderVM(thisOrder);
            }
            catch (Exception ex)
            {
                await dbTrans.RollbackAsync();
                throw new Exception("Could not create an order for this listing. Please, check: " + ex.Message);
            }
        }

        // look for a Listing Status based on the name
        private async Task<int> ValidateState(string name)
        {
            int returnStatus = await _listingStatusRepository.GetByName(name);

            if (returnStatus <= 0)
                throw new Exception("Could not find the requested status: " + name);

            return returnStatus;
        }

        // retrieves only the Status of a listing
        private async Task<int> GetStatus(Guid id)
        {
            var result = await _context.Listings.FirstOrDefaultAsync(lst => lst.Id == id);

            if (result == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            return result.ListingStatusId;
        }

        private async Task<decimal> GetPrice(Guid id)
        {
            var result = await _context.Listings.FindAsync(id);
            if (result == null)
                throw new NotFoundException(_notFoundMsg, id.ToString());

            return result.Price;
        }

        private async Task<decimal> TotalPrice(decimal listingPrice)
        {
            var result = listingPrice;
            var fees = await _context.Fees.Where(fee => fee.IsActive == true).ToListAsync();

            fees.ForEach(item =>
            {
                result += item.IsPercentual ? listingPrice * (item.Value/100) : item.Value;
            });

            return Math.Round(result, 2);
        }
    }
}
