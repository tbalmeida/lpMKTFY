using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class CategoryUpdateVM
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(20)]

        public string Title { get; set; }

    }
}
