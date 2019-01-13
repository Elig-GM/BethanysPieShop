namespace BethanysPieShop.Models
{
    public class LikeReview
    {
        public int LikeReviewId { get; set; }
        public string UserId { get; set;}
        public int PieReviewId { get; set; }
        public virtual PieReview PieReview { get; set; }
        // public List<Like> { get; set;}
        // public string Review { get; set; }
    }
}
