using MKTFY.Models.Entities;

namespace MKTFY.Models.ViewModels
{
    public class CityVM
    {
        public CityVM(City src)
        {
            Id = src.Id;
            Name = src.Name;
            ProvinceId = src.ProvinceId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ProvinceId { get; set; }
    }
}
