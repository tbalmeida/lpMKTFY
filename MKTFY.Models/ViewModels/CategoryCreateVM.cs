using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(20)]
        public string Title { get; set; }
    }
}
