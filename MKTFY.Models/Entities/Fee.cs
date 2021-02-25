using MKTFY.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Fee
    {
        public Fee() { }

        public Fee(FeeCreateVM src)
        {
            Title = src.Title;
            Value = src.Value;
            IsPercentual = src.IsPercentual;
            IsActive = src.IsActive;
            Cap = IsPercentual ? src.Cap : 0;
            Notes = src.Notes;
        }

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

        // Limits the value when a fee is percentual. If == 0, then there's no limit
        public decimal? Cap { get; set; }

        [MaxLength(100)]
        public string? Notes { get; set; }
    }
}
