using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Identity;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BethanysPieShop.Controllers
{
    [Route("api/[controller]")]
    public class AddUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddUserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // // GET: /<controller>/
        // public IActionResult Index()
        // {
        //     return View();
        // }

        // public Task GET()
        // {
        //     // return View();
        // }

        // [HttpPost]
        // public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        // {
        //     if (!ModelState.IsValid) return View(addUserViewModel);

        //     var user = new AppUser()
        //     {
        //         FirstName = addUserViewModel.Input.FirstName,
        //         LastName = addUserViewModel.Input.LastName,
        //         UserName = addUserViewModel.UserName,
        //         Email = addUserViewModel.Email,
        //         PhoneNumber = addUserViewModel.Input.PhoneNumber,
        //         Birthdate = addUserViewModel.Birthdate,
        //         State = addUserViewModel.State,
        //         City = addUserViewModel.City
        //     };

        //     IdentityResult result = await _userManager.CreateAsync(user, addUserViewModel.Password);

        //     if (result.Succeeded)
        //     {
        //         return RedirectToAction("UserManagement", _userManager.Users);
        //     }

        //     foreach (IdentityError error in result.Errors)
        //     {
        //         ModelState.AddModelError("", error.Description);
        //     }
        //     return View(addUserViewModel);
        // }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // if (!ModelState.IsValid) return View(addUserViewModel);
            // var serializer = new JavaScriptSerializer();
            string product = new StreamReader(Request.Body).ReadToEnd();

            var addUserViewModel = JsonConvert.DeserializeObject<AddUserViewModel>(product);
            var user = new AppUser()
            {
                FirstName = addUserViewModel.Input.FirstName,
                LastName = addUserViewModel.Input.LastName,
                UserName = addUserViewModel.UserName,
                Email = addUserViewModel.Email,
                PhoneNumber = addUserViewModel.Input.PhoneNumber,
                Birthdate = addUserViewModel.Birthdate,
                State = addUserViewModel.State,
                City = addUserViewModel.City
            };

            IdentityResult result = await _userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                // Json(new { success = true });
                return Ok(new { Result = true, Message = true });
            }
            return Ok(new { Result = false});
            // foreach (IdentityError error in result.Errors)
            // {
            //     ModelState.AddModelError("", error.Description);
            // }
            // return View(addUserViewModel);
        }
    }
}