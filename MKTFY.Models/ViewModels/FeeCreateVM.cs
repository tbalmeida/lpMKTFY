using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class FeeCreateVM
    {
        [Required, MinLength(3), MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public bool IsPercentual { get; set; } = false;

        [Required]
        public bool IsActive { get; set; } = true;

        [MaxLength(100)]
        public string? Notes { get; set; }
    }
}
