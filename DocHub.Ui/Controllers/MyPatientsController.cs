using DocHub.Core.Domain.Models;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.Enums.Views;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;


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
        private readonly IReferralsGetterService _referralsGetterService;
        private readonly IPatientsUpdaterService _patientsUpdater;
        public MyPatientsController(IPatientsGetterService patientsGetterService,
            IPatientsAdderService patientsAdderService,
            IPatientsSorterService patientsSorterService,
            IPatientsSearcherService patientsSearcherService, IAppointmentsGetterService appointmentsGetterService,
            IPrescriptionGetterService prescriptionGetterService, IReferralsGetterService referralsGetterService, IPatientsUpdaterService patientsUpdater)
        {
            _patientsGetterService = patientsGetterService;
            _patientsAdderService = patientsAdderService;
            _patientsSorterService = patientsSorterService;
            _patientsSearcherService = patientsSearcherService;
            _appointmentsGetterService = appointmentsGetterService;
            _prescriptionGetterService = prescriptionGetterService;
            _referralsGetterService = referralsGetterService;
            _patientsUpdater = patientsUpdater;
        }

        public async Task<IActionResult> Index(string? query,
            string sortBy = nameof(PatientResponse.FullName),
            SortOrderOptions sortOrder = SortOrderOptions.Asc)
        {
            var filteredPatients =
                await _patientsSearcherService.Search(query);
            var sortedPatients =
                await _patientsSorterService.GetSortedPatients(filteredPatients, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            ViewBag.CurrentQuery = query;
            ;
            return View(sortedPatients);
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null) return RedirectToAction("Index");
            var patient = await _patientsGetterService.Get(id);
            if (patient is null) return RedirectToAction("Index");
            var model = new PatientUpdateRequest()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PeselNumber = patient.PeselNumber,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                City = patient.City,
                PostalCode = patient.PostalCode,
                Address = patient.Address,
                TakenMedications = patient.TakenMedications,
                Allergies = patient.Allergies,
                HistoryOfDiseases = patient.HistoryOfDseases,
            };
            return View(model);

        }

        public async Task<IActionResult> Edit(PatientUpdateRequest? request)
        {
            if (request is null) return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                var updatedPatient = await _patientsUpdater.UpdatePatient(request);
            }

            return View(request);
        }

        [Route($"{{id:guid}}")]
        [HttpGet]
        public async Task<IActionResult> ShowProfile(Guid? id)
        {
            if (id is null) return RedirectToAction("Index");
            var matchingPatient = await _patientsGetterService.Get(id);
            if (matchingPatient is null) return RedirectToAction("Index");
            var patient = await _patientsGetterService.Get(id);
            var appointments = await _appointmentsGetterService.GetAllPatientsAppointments(matchingPatient.Id);
            var sortedAppointments = appointments.OrderBy(model =>
                    model.State == State.Finished ? 0 : (model.State == State.Reserved ? 1 : 2))
                .ThenByDescending(
                    app =>
                    {
                        if (app.Start != null) return app.Start.Value;
                        throw new ArgumentException();
                    });
            foreach (var item in appointments)
            {
                var prescriptions = _prescriptionGetterService.GetAllByAppointmentId(item.Id);
                var referrals = _referralsGetterService.GetAllByAppointmentId(item.Id);
                item.SetPrescriptions(prescriptions);
                item.SetReferrals(referrals);
            }

            var model = new Profile()
            {
                Patient = patient,
                Appointments = sortedAppointments
            };
            return View(model);
        }
    }
}