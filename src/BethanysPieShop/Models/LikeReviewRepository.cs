using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class LikeReviewRepository : ILikeReviewRepository
    {
        private readonly AppDbContext _appDbContext;

        public LikeReviewRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddLikeReview(LikeReview likeReview)
        {
            _appDbContext.LikesReview.Add(likeReview);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<LikeReview> GetLikeForReview(int reviewId)
        {
            return _appDbContext.LikesReview.Where(p => p.PieReview.PieReviewId == reviewId);
        }
    }
}
