using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    #nullable enable
    public interface IListingRepository
    {
        Task<ListingVM> Create(ListingCreateVM src);

        Task<ListingVM> Update(Guid id, ListingUpdateVM src);

        Task<ListingVM> GetById(Guid id);

        Task<string> Delete(Guid id);

        Task<List<Listing>> FilterListings(int cityId = 0, string searchText = null, int categoryId = 0, int itemConditionId = 0, int listingStatusId = 0, bool activeOnly = true, string ownerId = null);

        Task<List<ListingVM>> GetListings([Optional] int cityId, [Optional] string searchText, [Optional] int categoryId, [Optional] int itemConditionId, [Optional] int listingStatusId, [Optional] bool activeOnly, [Optional] string ownerId);

        Task<List<ListingShortVM>> GetListingsShort([Optional] int cityId, [Optional] string searchText, [Optional] int categoryId, [Optional] int itemConditionId, [Optional] int listingStatusId, [Optional] bool activeOnly, [Optional] string ownerId);

        Task<ListingVM> UpdatePrice(Guid id, ListingUpdatePriceVM src);

        Task<ListingVM> UpdateStatus(Guid id, ListingUpdateStatusVM src);

        Task<OrderVM> Buy(OrderCreateVM src);
    }
}
