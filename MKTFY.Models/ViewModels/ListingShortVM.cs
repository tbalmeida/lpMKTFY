using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ListingShortVM
    {

        public ListingShortVM(Listing src)
        {
            Id = src.Id;
            Title = src.Title;
            Price = src.Price;
        }


        public Guid Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}
