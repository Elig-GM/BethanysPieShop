using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
// using BethanysPieShop.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Identity;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BethanysPieShop.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        // private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            // _roleManager = roleManager;
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usr = await _userManager.GetUserAsync(User);

            var users = _userManager.Users;
            IEnumerable<AppUser> dbUsers = null;

            dbUsers = users;

            List<TabletUserViewModel> user = new List<TabletUserViewModel>();
            foreach (var dbUser in dbUsers)
            {
                if (usr.Id != dbUser.Id)
                {
                    user.Add(
                            new TabletUserViewModel()
                            {
                                id = dbUser.Id,
                                fullName = $"{dbUser.FirstName} {dbUser.LastName}",
                                email = dbUser.Email,
                                phoneNumber = dbUser.PhoneNumber,
                                username = dbUser.UserName,
                            }
                    );
                }
            }
            return Json(new { data = user });
        }

        // GET api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return RedirectToAction("UserManagement", _userManager.Users);

            var claims = await _userManager.GetClaimsAsync(user);

            var vm = new EditUserViewModel()
            {
                Input = new EditUserViewModel.InputModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber
                },
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Birthdate = user.Birthdate,
                State = user.State,
                City = user.City,
                UserClaims = claims.Select(c => c.Value).ToList()
            };

            return Json(vm, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid || addUserViewModel == null) return BadRequest(ModelState);

            // string product = new StreamReader(Request.Body).ReadToEnd();
            // var addUserViewModel = JsonConvert.DeserializeObject<AddUserViewModel>(product.ToString());

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

            var result = await _userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                return Ok(new { Result = true});
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("All", error.Description);
            }
            return BadRequest(ModelState);
            // return NotFound(addUserViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid || editUserViewModel == null)
                return BadRequest(ModelState);
                // ModelState.err

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.FirstName = editUserViewModel.Input.FirstName;
                user.LastName = editUserViewModel.Input.LastName;
                user.UserName = editUserViewModel.UserName;
                user.Email = editUserViewModel.Email;
                user.PhoneNumber = editUserViewModel.Input.PhoneNumber;
                user.Birthdate = editUserViewModel.Birthdate;
                user.State = editUserViewModel.State;
                user.City = editUserViewModel.City;

            }
            else
            {
                return NotFound(id);
            }
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Result = true });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("All", error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound($"Id not found: {'"'}{id}{'"'}");

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { Result = true, Message = true });
            }
            return BadRequest(result.Errors);
        }
    }
}