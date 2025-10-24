using HorrorOnline.Core.Domain.Entities.IdentityEntities;
using HorrorOnline.Core.DTO;
using HorrorOnline.Core.Enum;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HorrorOnline.UI.Controllers
{
    [Route("[controller]/[action]")]
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
        [ValidateAntiForgeryToken]
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

                return RedirectToAction(nameof(StoryController.Index), controllerName: "Story");
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

        [HttpGet]
        [Authorize("NotAuthenticated")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthenticated")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest, string? ReturnUrl)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(error => error.Errors).Select(error => error.ErrorMessage);

                return View(userLoginRequest);
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(userLoginRequest.UserName, userLoginRequest.Password, isPersistent: userLoginRequest.RememberMe, lockoutOnFailure: false);

            if (signInResult.Succeeded == false)
            {
                string loginError = string.Empty;

                if (signInResult.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "Acceso bloqueado");
                }
                if (signInResult.RequiresTwoFactor)
                {
                    ModelState.AddModelError("Login", "Requiere acceso de doble factor");
                }
                if (signInResult.IsNotAllowed)
                {
                    ModelState.AddModelError("Login", "Invalid email or password.");
                }

                return View(userLoginRequest);
            }

            if (string.IsNullOrEmpty(ReturnUrl) == false &&
    Url.IsLocalUrl(ReturnUrl))
            {
                //For security reasons, it has to be local so other details about the log in are not sent to other websites.
                return LocalRedirect(ReturnUrl);
            }

            return RedirectToAction(nameof(StoryController.Index), controllerName: "Story");
        }

        [HttpGet]
        public async Task<ActionResult> LogOut(string? ReturnUrl)
        {
            await _signInManager.SignOutAsync();

            if (string.IsNullOrEmpty(ReturnUrl) == false &&
Url.IsLocalUrl(ReturnUrl))
            {
                //For security reasons, it has to be local so other details about the log in are not sent to other websites.
                return LocalRedirect(ReturnUrl);
            }

            return RedirectToAction(nameof(StoryController.Index), controllerName: "Story");
        }

        #region VALIDATION_CALLS
        /// <summary>
        /// This is called from the form (client side) to check if the email they are entering is already registered
        /// </summary>
        /// <param name="email">Email to be checked</param>
        /// <returns>Whether or not the email can be added in the user database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            return Json(user is null);
        }

        /// <summary>
        /// This is called from the form (client side) to check if the userName they are entering is already registered
        /// </summary>
        /// <param name="userName">userName to be checked</param>
        /// <returns>Whether or not the userName can be added in the user database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> IsUserNameAlreadyRegistered(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            return Json(user is null);
        }
        #endregion
    }
}
