using DocHub.Core.DTO;
using DocHub.Core.Enums.Views;
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
        private readonly IPatientsSorterService _patientsSorterService;
        public MyPatientsController(IPatientsGetterService patientsGetterService,
            IPatientsAdderService patientsAdderService,
            IPatientsSorterService patientsSorterService)
        {
            _patientsGetterService = patientsGetterService;
            _patientsAdderService = patientsAdderService;
            _patientsSorterService = patientsSorterService;
        }
        public async Task<IActionResult> Index(
            string sortBy = nameof(PatientResponse.FullName),
            SortOrderOptions sortOrder = SortOrderOptions.Asc)
        {
            var  allPatients = await _patientsGetterService.GetAll();
            //TODO: Popraw ten null reference
            var sortedPatients =
                await _patientsSorterService.GetSortedPatients(allPatients, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            return View(sortedPatients.ToList());
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
                return RedirectToAction(nameof(MyPatientsController.Index));
            }
            return View(request);
        }
    }
}
