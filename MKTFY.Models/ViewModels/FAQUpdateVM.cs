using MKTFY.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class FAQUpdateVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(200), MinLength(10)]
        public string Title { get; set; }

        [Required, MaxLength(5000), MinLength(10)]
        public string Text { get; set; }
    }
}
