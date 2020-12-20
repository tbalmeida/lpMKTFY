﻿using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IListingRepository
    {
        Task<ListingVM> Create(ListingCreateVM src);

        Task<ListingVM> Update(Guid id, ListingUpdateVM src);

        Task<ListingVM> GetById(Guid id);

        Task<string> Delete(Guid id);

        Task<List<ListingVM>> GetAll(int status = 1, string searchText = null, int? filterCategory = null, Guid? owner = null);

        Task<bool> UpdatePrice(Guid id, decimal newPrice);

        Task<bool> UpdateStatus(Guid id, int newStatus);
    }
}