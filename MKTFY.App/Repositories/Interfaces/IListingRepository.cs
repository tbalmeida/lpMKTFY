using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IListingRepository
    {
        Task<ListingVM> Create(ListingCreateVM src);

        Task<ListingVM> Update(Guid id, ListingUpdateVM src);

        Task<ListingVM> GetById(Guid id);

        Task<string> Delete(Guid id);

        Task<List<ListingVM>> GetListings(string searchText, int? searchCity, int? searchStatus, int? searchCategory, int? searchItemCond, string? owner);

        Task<List<ListingShortVM>> GetListingsShort(string searchText, int? searchCity, int? searchStatus, int? searchCategory, int? searchItemCond, string? owner);

        Task<ListingVM> UpdatePrice(Guid id, ListingUpdatePriceVM src);

        Task<ListingVM> UpdateStatus(Guid id, ListingUpdateStatusVM src);
    }
}
