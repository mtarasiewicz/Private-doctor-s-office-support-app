using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Core.DTO;
using DocHub.Core.Enums;
using DocHub.Core.Enums.Identity;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocHub.Ui.Controllers
{
    /// <summary>
    /// Controller supporting authorization mechanisms
    /// </summary>
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IPatientsAdderService _patientsAdderService;
        public AccountController
            (UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IPatientsAdderService patientsAdderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _patientsAdderService = patientsAdderService;
        }
        /// <summary>
        /// Controller action method returning the registration form view, HttpGet
        /// </summary>
        /// <returns>Register view</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Action method handling user registrations, HttpPost
        /// </summary>
        /// <param name="request"></param>
        /// <returns>If registration is successful, it returns the dashboard view, otherwise it returns the registration view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            //Validate the model, if model is invalid return view
            var exists = await _userManager.FindByEmailAsync(request.Email);
            if (exists is not null)
            {
                ModelState.AddModelError(string.Empty, "Email is already in use");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Errors =
                    ModelState
                    .Values
                    .SelectMany(errors => errors.Errors)
                    .Select(error => error.ErrorMessage);
                return View(request);
            }

            var appUser = new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
            };
           

            IdentityResult registerResult = await _userManager.CreateAsync(appUser, request.Password);
            if (registerResult.Succeeded)
            {
                if (request.Role == UserRoles.Admin)
                {
                    if (await _roleManager.FindByNameAsync(UserRoles.Admin.ToString()) is null)
                    {
                        var adminRole = new ApplicationRole()
                        {
                            Name = UserRoles.Admin.ToString()
                        };
                        await _roleManager.CreateAsync(adminRole);
                    }
                    await _userManager.AddToRoleAsync(appUser, UserRoles.Admin.ToString());
                }
                else if (request.Role == UserRoles.Doctor)
                {
                    if (await _roleManager.FindByNameAsync(UserRoles.Doctor.ToString()) is null)
                    {
                        var doctorRole = new ApplicationRole()
                        {
                            Name = UserRoles.Doctor.ToString()
                        };
                        await _roleManager.CreateAsync(doctorRole);
                    }
                    await _userManager.AddToRoleAsync(appUser, UserRoles.Doctor.ToString());
                }
                else
                {
                    if (await _roleManager.FindByNameAsync(UserRoles.Patient.ToString()) is null)
                    {
                        var patientRole = new ApplicationRole()
                        {
                            Name = UserRoles.Patient.ToString()
                        };
                        await _roleManager.CreateAsync(patientRole);
                    }
                    var patientAccount = new PatientAddRequest()
                    {
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        Email = appUser.Email,
                        UserId = appUser.Id,
                    };
                    await _patientsAdderService.AddPatient(patientAccount);
                    await _userManager.AddToRoleAsync(appUser, UserRoles.Patient.ToString());
                    ViewBag.Heading = "Add some aditional data.";
                    ViewBag.Submit = "Save";
                    ViewBag.Skip = "Skip";
                    await _signInManager.SignInAsync(appUser, isPersistent: false);
                    TempData["SuccessMessage"] = "Registration successful.";
                    return RedirectToAction(nameof(PatientController.EditProfile), "Patient", new { heading = "Add some aditional data.", 
                    submit = "Save", skip = "Skip", register = true});
                }
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
            else
            {
                foreach (var error in registerResult.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(request);
            }
        }
        /// <summary>
        /// Action method returnig the log in form view, HttpGet
        /// </summary>
        /// <returns>Log in view</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }
        /// <summary>
        /// Action method handling user log in, HttpPost
        /// </summary>
        /// <param name="request"></param>
        /// <returns>If log in is succesful, it return the dashboard view, otherwise it returns the log in view with error message</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors =
                    ModelState
                    .Values
                    .SelectMany(errors => errors.Errors)
                    .Select(error => error.ErrorMessage);
                return View(request);
            }
            if (request.Email == null || request.Password == null)
            {
                throw new ArgumentNullException();
            }
            var logInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
            if (logInResult.Succeeded)
            {
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
            ViewBag.Error = "Invalid email or password";
            return View(request);
        }
        /// <summary>
        /// Authorize action method handling log out, only log in users can see that, and use that
        /// </summary>
        /// <returns>Main view for anomyous users</returns>
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }
    }
}
