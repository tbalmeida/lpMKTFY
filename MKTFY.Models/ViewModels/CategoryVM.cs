using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM(Category src)
        {
            Id = src.Id;
            Title = src.Title;
        }

        public int Id { get; set; }

        public string Title { get; set; }

    }
}
