using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30), MinLength(3)]
        public string Name { get; set; }

        [Required, MaxLength(2), MinLength(2)]
        public string Abbreviation { get; set; }

        [Required]
        public int CountryId { get; set; }

        // Navigational
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
