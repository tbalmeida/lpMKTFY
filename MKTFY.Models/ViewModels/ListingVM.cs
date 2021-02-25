using MKTFY.Models.Entities;
using System;

namespace MKTFY.Models.ViewModels
{
    public class ListingVM
    {

        public ListingVM(Listing src, int activeListings = 0)
        {
            Id = src.Id;
            CategoryId = src.CategoryId;
            CategoryTitle = src.Category?.Title;
            ItemConditionId = src.ItemConditionId;
            ItemConditionName = src.ItemCondition?.Name;
            Title = src.Title;
            Description = src.Description;
            UserId = src.UserId;
            CityId = src.CityId;
            City = src.City == null ? null : src.City.Name + ", " + src.City.Province.Abbreviation;
            Location = src.Location;
            Price = src.Price;
            ListingStatusId = src.ListingStatusId;
            StatusName = src.ListingStatus?.Name;
            IsActive = src.ListingStatus.IsActive;
            Created = src.Created;
            if (src.Updated != null)
            {
                Updated = (DateTime)src.Updated;
            }
            Seller = src.User == null ? 
                null : 
                src.User.FirstName + " " + src.User.LastName;
            DaysPosted = DaysDiff() == 0 ? 
                "Posted today" : 
                "Posted " + DaysDiff().ToString() + " days ago";
            ActiveListCount = activeListings;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public int ItemConditionId { get; set; }

        public string ItemConditionName { get; set; }

        public int ListingStatusId { get; set; }

        public string StatusName { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Seller { get; set; }

        public int CityId { get; set; }

        public string City { get; set; }

        public string Location { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string DaysPosted { get; }

        private int DaysDiff()
        {
            TimeSpan qtDays = DateTime.Now - Created;
            return qtDays.Days;
        }

        public bool IsActive { get; set; }

        public int ActiveListCount { get; set; }
    }
}
