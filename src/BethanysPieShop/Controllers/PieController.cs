using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieReviewRepository _pieReviewRepository;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ILogger<PieController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository, ILogger<PieController> logger,
            IPieReviewRepository pieReviewRepository, HtmlEncoder htmlEncoder, UserManager<AppUser> userManager)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _pieReviewRepository = pieReviewRepository;
            _htmlEncoder = htmlEncoder;
            _logger = logger;
            _userManager = userManager;
        }

        //public ViewResult List()
        //{
        //    PiesListViewModel piesListViewModel = new PiesListViewModel();
        //    piesListViewModel.Pies = _pieRepository.Pies;

        //    piesListViewModel.CurrentCategory = "Cheese cakes";

        //    return View(piesListViewModel);
        //}

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.Pies.OrderBy(p => p.Name).OrderByDescending(s => s.InStock);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.Pies.Where(p => p.Category.CategoryName == category && p.InStock == true)
                   .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        [Route("[controller]/Details/{id}")]
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            // var or = pie.PieReviews.OrderByDescending(p => p.PieReviewId);
            // pie.PieReviews = or;
            return View(new PieDetailViewModel() { Pie = pie });
        }

        [Route("[controller]/Details/{id}")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(int id, string review)
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
            
            return View(new PieDetailViewModel() { Pie = pie });
        }

    }
}
