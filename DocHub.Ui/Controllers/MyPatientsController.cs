using DocHub.Core.DTO;
using DocHub.Core.Enums.Views;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;

namespace DocHub.Ui.Controllers
{
    [Authorize(Roles = "Doctor, Admin")]
    [Route("[controller]/[action]")]
    public class MyPatientsController : Controller
    {
        private readonly IPatientsGetterService _patientsGetterService;
        private readonly IPatientsAdderService _patientsAdderService;
        private readonly IPatientsSorterService _patientsSorterService;
        private readonly IPatientsSearcherService _patientsSearcherService;
        public MyPatientsController(IPatientsGetterService patientsGetterService,
            IPatientsAdderService patientsAdderService,
            IPatientsSorterService patientsSorterService,
            IPatientsSearcherService patientsSearcherService)
        {
            _patientsGetterService = patientsGetterService;
            _patientsAdderService = patientsAdderService;
            _patientsSorterService = patientsSorterService;
            _patientsSearcherService = patientsSearcherService;
        }
        public async Task<IActionResult> Index(string? query,
            string sortBy = nameof(PatientResponse.FullName),
            SortOrderOptions sortOrder = SortOrderOptions.Asc)
        {
            //TODO: Popraw ten null reference
            var filteredPatients = 
                await _patientsSearcherService.Search(query);
            var sortedPatients =
                await _patientsSorterService.GetSortedPatients(filteredPatients, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            ViewBag.CurrentQuery = query;
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
