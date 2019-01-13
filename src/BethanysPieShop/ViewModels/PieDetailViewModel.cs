using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    public class PieDetailViewModel
    {
        public Pie Pie { get; set; }
        
        [Required]
        public string Review { get; set; }
    }
}
