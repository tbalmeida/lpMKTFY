using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class FAQCreateVM
    {
        [Required, MaxLength(200), MinLength(10)]
        public string Title { get; set; }

        [Required, MaxLength(5000), MinLength(10)]
        public string Text { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
