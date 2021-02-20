using MKTFY.Models.Entities;

namespace MKTFY.Models.ViewModels
{
    public class FeeVM
    {
        public FeeVM(Fee src)
        {
            Id = src.Id;
            Title = src.Title;
            Value = src.Value;
            IsPercentual = src.IsPercentual;
            IsActive = src.IsActive;
            Notes = src.Notes;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Value { get; set; }

        public bool IsPercentual { get; set; }

        public bool IsActive { get; set; }

        public string? Notes { get; set; }
    }
}
