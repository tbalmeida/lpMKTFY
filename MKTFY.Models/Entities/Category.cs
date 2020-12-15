using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Category
    {
        public Category() { }

        public Category(CategoryCreateVM src)
        {
            Title = src.Title;
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Title { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}
