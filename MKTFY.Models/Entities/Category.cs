using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Title { get; set; }

        public bool Active { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}
