using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.ViewModels
{
    public class TabletUserViewModel
    {
        public string username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string fullName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Phone]
        public string phoneNumber { get; set; }

        // [Id]
        public string id { get; set; }
    }
}