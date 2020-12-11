using MKTFY.Models.Entities;
using System;

namespace MKTFY.Models.ViewModels
{
    public class FAQVM
    {
        public FAQVM (FAQ src)
        {
            Id = src.Id;
            Title = src.Title;
            Text = src.Text;
            Created = src.Created;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }
    }
}
