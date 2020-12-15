using MKTFY.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM(Category src)
        {
            Id = src.Id;
            Title = src.Title;
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]

        public string Title { get; set; }

    }
}
