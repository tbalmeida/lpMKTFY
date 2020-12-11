using MKTFY.Models.Entities;
using System;

namespace MKTFY.Models.ViewModels
{
    public class FAQUpdateVM : FAQCreateVM
    {
        public FAQUpdateVM(FAQ src)
        {
            Id = src.Id;
            Title = src.Title;
            Text = src.Text;
        }

        public Guid Id { get; set; }
    }
}
