using System.Collections.Generic;
using BethanysPieShop.Identity;

namespace BethanysPieShop.Models
{
    public class PieReview
    {
        public int PieReviewId { get; set; }
        public string UserName { get; set;}
        public string UserId { get; set;}
        // public AppUser UserReview { get; set;}
        public Pie Pie { get; set; }
        // public int Likes { get; set;}
        public string Review { get; set; }
        public virtual List<LikeReview> Likes { get; set;}
    }
}
