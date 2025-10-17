using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HorrorOnline.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize("NotAuthenticated")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            return Json(user is null);
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsUserNameAlreadyRegistered(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            return Json(user is null);
        }
    }
}
