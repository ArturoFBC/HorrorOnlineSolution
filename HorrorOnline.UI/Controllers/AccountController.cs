using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HorrorOnline.UI.Controllers
{
    [Route("[controller]")]
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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthenticated")]
        public async Task<IActionResult> Register(UserAddRequest userAddRequest)
        {

            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(error => error.Errors).Select(error => error.ErrorMessage);

                return View(userAddRequest);
            }

            ApplicationUser newUser = userAddRequest.ToApplicationUser();

            foreach (IPasswordValidator<ApplicationUser> passwordValidator in _userManager.PasswordValidators)
            {
                IdentityResult passwordCheckResult = await passwordValidator.ValidateAsync(_userManager, newUser, userAddRequest.Password);

                bool errorFound = false;
                List<IdentityError> errorList = new List<IdentityError>();
                if (!passwordCheckResult.Succeeded)
                {
                    errorFound = true;
                    errorList.AddRange(passwordCheckResult.Errors);
                }

                ViewBag.Errors = errorList.Select(error => $"{error.Code}: {error.Description}");

                if (errorFound)
                {
                    return View(userAddRequest);
                }
            }

            IdentityResult userAddResult = await _userManager.CreateAsync(newUser, userAddRequest.Password);

            if (userAddResult.Succeeded)
            {
                await CheckAndCreateRole(userAddRequest.UserType);

                await _userManager.AddToRoleAsync(newUser, userAddRequest.UserType.ToString());

                await _signInManager.SignInAsync(newUser, isPersistent: false);

                return RedirectToAction(nameof(StoryController.Index), "Persons");
            }
            else
            {
                foreach (var error in userAddResult.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(userAddRequest);
            }
        }

        private async Task CheckAndCreateRole(UserTypeRole role)
        {
            if (await _roleManager.FindByNameAsync(role.ToString()) is null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = role.ToString(),
                };
                await _roleManager.CreateAsync(applicationRole);
            }
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
