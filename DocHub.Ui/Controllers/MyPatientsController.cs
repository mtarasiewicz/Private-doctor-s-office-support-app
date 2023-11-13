using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocHub.Ui.Controllers
{
    [Authorize(Roles = "Doctor, Admin")]
    [Route("[controller]/[action]")]
    public class MyPatientsController : Controller
    {
        private readonly IPatientsGetterService _patientsGetterService;
        private readonly IPatientsAdderService _patientsAdderService;
        public MyPatientsController(IPatientsGetterService patientsGetterService, IPatientsAdderService patientsAdderService)
        {
            _patientsGetterService = patientsGetterService;
            _patientsAdderService = patientsAdderService;
        }
        public async Task<IActionResult> MyPatients()
        {
            var allPatients = await _patientsGetterService.GetAll();

            return View(allPatients);
        }
        [HttpGet]
        public IActionResult CreatePatient()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatient(PatientAddRequest request)
        {
            if (ModelState.IsValid)
            {
                var addedPatient = await _patientsAdderService.AddPatient(request);
                TempData["SuccessMessage"] = "Patient added.";
                return RedirectToAction(nameof(MyPatientsController.MyPatients));
            }
            return View(request);
        }
    }
}
