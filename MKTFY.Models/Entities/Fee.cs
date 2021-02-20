using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public bool IsPercentual { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [MaxLength(100)]
        public string? Notes { get; set; }
    }
}
