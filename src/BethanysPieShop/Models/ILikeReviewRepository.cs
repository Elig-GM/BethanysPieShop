using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public interface ILikeReviewRepository
    {
        void AddLikeReview(LikeReview likeReview);
        IEnumerable<LikeReview> GetLikeForReview(int reviewId);
    }
}
