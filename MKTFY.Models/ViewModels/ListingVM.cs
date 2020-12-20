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
            Title = src.Title;
            Description = src.Description;
            CategoryId = src.CategoryId;
            Price = src.Price;
            UserId = src.UserId;
            CityId = src.CityId;
            ListingStatusId = src.ListingStatusId;
            Created = src.Created;
            Updated = (DateTime)src.Updated;
        }


        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public int CityId { get; set; }

        public int ListingStatusId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
