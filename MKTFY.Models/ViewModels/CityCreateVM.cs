using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class CityCreateVM
    {
        [Required, MaxLength(50), MinLength(3)]
        public string Name { get; set; }

        [Required]
        public int ProvinceId { get; set; }
    }
}
