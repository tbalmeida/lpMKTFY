using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ItemConditionVM
    {
        public ItemConditionVM(ItemCondition src)
        {
            Id = src.Id;
            Name = src.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
