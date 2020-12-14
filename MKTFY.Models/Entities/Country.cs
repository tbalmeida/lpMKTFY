using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(3)]
        public string Name { get; set; }

        [MaxLength(3), MinLength(2)]
        public string Abbreviation { get; set; }

        // Nav
        public ICollection<Province> Provinces { get; set; }
    }
}
