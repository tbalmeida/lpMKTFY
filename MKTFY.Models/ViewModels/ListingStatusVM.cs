using MKTFY.Models.Entities;

namespace MKTFY.Models.ViewModels
{
    public class ListingStatusVM
    {
        public ListingStatusVM(ListingStatus src)
        {
            Id = src.Id;
            Name = src.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
