using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingVM
    {

        public ListingVM(Listing src)
        {
            Id = src.Id;
            CategoryId = src.CategoryId;
            CategoryTitle = src.Category == null ? null : src.Category.Title;
            ItemConditionId = src.ItemConditionId;
            ItemConditionName = src.ItemCondition == null ? null : src.ItemCondition.Name;
            Title = src.Title;
            Description = src.Description;
            Price = src.Price;
            UserId = src.UserId;
            CityId = src.CityId;
            City = src.City == null ? null : src.City.Name;
            Location = src.Location;
            ListingStatusId = src.ListingStatusId;
            StatusName = src.ListingStatus == null ? null : src.ListingStatus.Name;
            Created = src.Created;
            if (src.Updated != null)
            {
                Updated = (DateTime)src.Updated;
            }
        }


        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public int CityId { get; set; }

        public string City { get; set; }

        public int ListingStatusId { get; set; }

        public string StatusName { get; set; }

        public int ItemConditionId { get; set; }

        public string ItemConditionName { get; set; }

        public string Location { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string CategoryTitle { get; set; }
    }
}
