using MKTFY.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class FAQ
    {
        public FAQ() { }

        public FAQ(FAQCreateVM src)
        {
            Title = src.Title;
            Text = src.Text;
            Created = src.Created;
        }

        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(200), MinLength(10)]
        public string Title { get; set; }

        [Required, MaxLength(5000), MinLength(10)]
        public string Text { get; set; }

        public DateTime Created { get; set; }
    }
}
