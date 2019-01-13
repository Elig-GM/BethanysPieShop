using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using BethanysPieShop.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Text.Encodings.Web;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    [Route("api/[controller]")]
    public class PieDataController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ILikeReviewRepository _likeReviewRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPieReviewRepository _pieReviewRepository;
        private readonly HtmlEncoder _htmlEncoder;

        public PieDataController(IPieRepository pieRepository, ILikeReviewRepository likeReviewRepository, UserManager<AppUser> userManager,
            IPieReviewRepository pieReviewRepository, HtmlEncoder htmlEncoder)
        {
            _pieRepository = pieRepository;
            _likeReviewRepository = likeReviewRepository;
            _userManager = userManager;
            _pieReviewRepository = pieReviewRepository;
            _htmlEncoder = htmlEncoder;

        }

        [HttpGet]
        public IEnumerable<PieViewModel> LoadMorePies()
        {
            IEnumerable<Pie> dbPies = null;

            dbPies = _pieRepository.Pies.OrderBy(p => p.PieId).Take(10);

            List<PieViewModel> pies = new List<PieViewModel>();

            foreach (var dbPie in dbPies)
            {
                pies.Add(MapDbPieToPieViewModel(dbPie));
            }
            return pies;
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Review(int id, [FromBody]string review)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
            {
                // _logger.LogWarning(LogEventIds.GetPieIdNotFound, new Exception("Pie not found"), "Pie with id {0} not found", id);
                return NotFound();
            }
            if (string.IsNullOrEmpty(review) || review == String.Empty)
            {
                ModelState.AddModelError("", "review empty");
            }else{
                var user = await _userManager.GetUserAsync(User);
                string encodedReview = _htmlEncoder.Encode(review);

                _pieReviewRepository.AddPieReview(
                    new PieReview() { 
                            Pie = pie, 
                            // UserReview = user,
                            UserName = $"{user.FirstName} {user.LastName}",
                            UserId = user.Id,
                            Review = encodedReview 
                        });
            }
            

            // var re = Ok(pie.PieReviews.OrderByDescending(p => p.PieReviewId));
            return Json(new {review = pie.PieReviews.OrderByDescending(p => p.PieReviewId)});
            // pie.PieReviews = or;
            // return View(new PieDetailViewModel() { Pie = pie });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            // var pie = _pieRepository.GetPieById(pieReviewId);
            // if (pie == null)
            //     return NotFound();

            // return View(new PieDetailViewModel() { Pie = pie });
            // var review = _pieReviewRepository.
            var user = await _userManager.GetUserAsync(User);
            _likeReviewRepository.AddLikeReview( 
                new LikeReview(){
                    UserId = user.Id,
                    PieReviewId = id
                });
            return Ok(new{});
        }

        private PieViewModel MapDbPieToPieViewModel(Pie dbPie)
        {
            return new PieViewModel()
            {
                PieId = dbPie.PieId,
                Name = dbPie.Name,
                Price = dbPie.Price,
                ShortDescription = dbPie.ShortDescription,
                ImageThumbnailUrl = dbPie.ImageThumbnailUrl
            };
        }
    }
}
