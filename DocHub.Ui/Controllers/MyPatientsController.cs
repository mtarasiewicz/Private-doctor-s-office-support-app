using DocHub.Core.Domain.Models;
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
        private readonly IAppointmentsGetterService _appointmentsGetterService;
        private readonly IPrescriptionGetterService _prescriptionGetterService;
        public MyPatientsController(IPatientsGetterService patientsGetterService,
            IPatientsAdderService patientsAdderService,
            IPatientsSorterService patientsSorterService,
            IPatientsSearcherService patientsSearcherService, IAppointmentsGetterService appointmentsGetterService, IPrescriptionGetterService prescriptionGetterService)
        {
            _patientsGetterService = patientsGetterService;
            _patientsAdderService = patientsAdderService;
            _patientsSorterService = patientsSorterService;
            _patientsSearcherService = patientsSearcherService;
            _appointmentsGetterService = appointmentsGetterService;
            _prescriptionGetterService = prescriptionGetterService;
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

        [Route("{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> ShowProfile(Guid? id)
        {   
            if (id is null) return RedirectToAction("Index");
            var matchingPatient = await _patientsGetterService.Get(id);
            if (matchingPatient is null) return RedirectToAction("Index");
            var patient = await _patientsGetterService.Get(id);
            var appointments =await _appointmentsGetterService.GetAllPatientsAppointments(matchingPatient.Id);
            foreach (var item in appointments)
            {
                var prescriptions =  _prescriptionGetterService.GetAllByAppointmentId(item.Id);
                item.SetPrescriptions(prescriptions);
            }
            var model = new Profile()
            {
                Patient = patient,
                Appointments = appointments
            };
            return Json(model);
        }
    }
}
