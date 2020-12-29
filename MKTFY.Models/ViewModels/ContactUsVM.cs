using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ContactUsVM
    {
        [Required]
        [EmailAddress]
        public string From { get; set; }

        [Required]
        public string FullName{ get; set; }

        public string Subject { get; set; } = "MKTFY - Contact Us";

        public string To { get; set; } = Environment.GetEnvironmentVariable("EmailTo");

        [Required]
        public string TextContent { get; set; }
    }
}
