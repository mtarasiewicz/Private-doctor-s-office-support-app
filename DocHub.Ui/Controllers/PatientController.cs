using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace DocHub.Ui.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PatientController : Controller
    {
        private readonly IPatientsGetterService _patientsGetterService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientsUpdaterService _patientsUpdaterService;

        public PatientController
            (IPatientsGetterService patientsGetterService,
            UserManager<ApplicationUser> userManager,
            IPatientsUpdaterService patientsUpdaterService)
        {
            _patientsGetterService = patientsGetterService;
            _userManager = userManager;
            _patientsUpdaterService = patientsUpdaterService;

        }
        [HttpGet]
        public async Task<IActionResult> EditProfile(string? heading, string? submit, string? skip, bool register = false)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) { return RedirectToAction(nameof(DashboardController.Index), "Dashboard"); }
            var patientProfile = await _patientsGetterService.GetByUserId(Guid.Parse(userId));
            if (patientProfile == null) { return RedirectToAction(nameof(DashboardController.Index), "Dashboard"); }
            var model = new PatientUpdateRequest()
            {
                Id = patientProfile.Id,
                FirstName = patientProfile.FirstName,
                LastName = patientProfile.LastName,
                Email = patientProfile.Email,
                PeselNumber = patientProfile.PeselNumber,
                PhoneNumber = patientProfile.PhoneNumber,
                DateOfBirth = patientProfile.DateOfBirth,
                City = patientProfile.City,
                PostalCode = patientProfile.PostalCode,
                Address = patientProfile.Address,
                TakenMedications = patientProfile.TakenMedications,
                Allergies = patientProfile.Allergies,
                HistoryOfDiseases = patientProfile.HistoryOfDseases,
                UserId = Guid.Parse(userId),
            };
            if (register)
            {
                ViewBag.Heading = heading;
                ViewBag.Submit = submit;
                ViewBag.Skip = skip;

            }
            else
            {
                ViewBag.Heading = $"Edit {model.FirstName} {model.LastName}";
                ViewBag.Submit = "Save";
                ViewBag.Skip = "Cancel";
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(PatientUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request is null) throw new ArgumentNullException();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId is null) throw new ArgumentNullException();
                var user = await _userManager.FindByIdAsync(userId) ?? throw new ArgumentNullException();
                user.Email = request.Email;
                user.UserName = request.Email;
                user.FirstName = request.FirstName ?? "";
                user.LastName = request.LastName ?? "";
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _patientsUpdaterService.UpdatePatient(request);
                    TempData["SuccessMessage"] = $"{request.FirstName} {request.LastName} profile saved.";
                    return View();
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }

            }
            return View(request);
        }
        


    }
}
