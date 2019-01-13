using BethanysPieShop.Identity;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            _userManager.GetUserName(User);
            // _userManager.
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(loginViewModel.ReturnUrl);
                }
            }   

            ModelState.AddModelError("", "Username/password not found");
            return View(loginViewModel);
        }

        // public IActionResult Manager(string returnUrl)
        // {
        //     var user = _userManager.GetUserAsync(User);
        //     if (user == null)
        //     {
        //         return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //     }

        //     var userName = _userManager.GetUserNameAsync(user);
        //     var email = _userManager.GetEmailAsync(user);
        //     var phoneNumber = _userManager.GetPhoneNumberAsync(user);

        //     var input = new InputModel
        //     {
        //         Email = email,
        //         PhoneNumber = phoneNumber
        //     };

        //     var vm = new UserViewModel() { Id = user.Id, Email = user.Email, UserName = user.UserName, UserClaims = claims.Select(c => c.Value).ToList() };

        //     return View(vm);
        //     // return View();
        // }

        [Route("Account/Profile")]
        public async Task<IActionResult> Profile()
        {
            // var user = await _userManager.FindByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null)
                return RedirectToAction("Index", "Home");

            // var input = new UserViewModel.InputModel
            // {
            //     FirstName = user.FirstName,
            //     LastName = user.LastName,
            //     Email = user.Email,
            //     PhoneNumber = user.PhoneNumber
            // };

            var EmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            var vm = new UserViewModel() 
            {
                Username = user.UserName, 
                Role = roles[0], 
                IsEmailConfirmed = EmailConfirmed, 
                Input = new UserViewModel.InputModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                }
            };

            return View(vm);
        }

        [Route("Account/Manage")]
        // [ViewComponent = ""]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            // var user = await _userManager.FindByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
                return RedirectToAction("Index", "Home");

            // var input = new UserViewModel.InputModel
            // {
            //     FirstName = user.FirstName,
            //     LastName = user.LastName,
            //     Email = user.Email,
            //     PhoneNumber = user.PhoneNumber
            // };

            var EmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            var vm = new UserViewModel() 
            {
                Username = user.UserName, 
                IsEmailConfirmed = EmailConfirmed, 
                Input = new UserViewModel.InputModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                }
            };

            return View(vm);
        }

        // [Route(Manager/Index)]
        [HttpPost]
        public async Task<IActionResult> Manage(UserViewModel userViewModel)
        {
            // var user = await _userManager.FindByIdAsync(userViewModel.Id);
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                user.Email = userViewModel.Input.Email;
                user.FirstName = userViewModel.Input.FirstName;
                user.LastName = userViewModel.Input.LastName;
                user.PhoneNumber = userViewModel.Input.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

               foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(userViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ChangePassword(){
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
            var vm = new ChangePasswordViewModel() {StatusMessage = ""};
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword){
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePassword.Input.OldPassword, changePassword.Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                changePassword.StatusMessage = "";
                return View(changePassword);
            }

            await _signInManager.RefreshSignInAsync(user);
            // _logger.LogInformation("User changed their password successfully.");
            changePassword.StatusMessage = "Your password has been changed.";

            // return RedirectToPage();

            return View(changePassword);
        }


        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser() { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                await _userManager.AddToRoleAsync(user, "Users");

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
