using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Core.Domain.Models;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Ui.Session;
using Hangfire;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;

namespace DocHub.Ui.Controllers;

[Route("[controller]/[action]")]
public class AppointmentsController : Controller
{
    private readonly IAppointmentsGetterService _appointmentsGetterService;
    private readonly IAppointmentsAdderService _appointmentsAdderService;
    private readonly IAppointmentsBookerService _appointmentsBookerService;
    private readonly IPatientsGetterService _patientsGetterService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IAppointmentsAddRangeService _appointmentsAddRangeService;
    private readonly IAppointmentUpdaterService _appointmentUpdaterService;
    private readonly IEmailSenderService _emailSender;
    private readonly IPrescriptionAdderService _prescriptionAdderService;
    private readonly IPrescriptionGetterService _prescriptionGetterService;
    private readonly IAppointmentsDeleterService _appointmentsDeleterService;
    private readonly IReferralsGetterService _referralsGetterService;
    private readonly IReferralsAdderService _referralsAdderService;

    public AppointmentsController(IAppointmentsGetterService appointmentsGetterService,
        IAppointmentsAdderService appointmentsAdderService, IAppointmentsBookerService appointmentsBookerService,
        IPatientsGetterService patientsGetterService, UserManager<ApplicationUser> userManager,
        IAppointmentsRepository appointmentsRepository, IAppointmentsAddRangeService appointmentsAddRangeService,
        IAppointmentUpdaterService appointmentUpdaterService, IEmailSenderService emailSender,
        IPrescriptionAdderService prescriptionAdderService, IPrescriptionGetterService prescriptionGetterService,
        IAppointmentsDeleterService appointmentsDeleterService,
        IReferralsGetterService referralsGetterService, IReferralsAdderService referralsAdderService)
    {
        _appointmentsGetterService = appointmentsGetterService;
        _appointmentsAdderService = appointmentsAdderService;
        _appointmentsBookerService = appointmentsBookerService;
        _patientsGetterService = patientsGetterService;
        _userManager = userManager;
        _appointmentsRepository = appointmentsRepository;
        _appointmentsAddRangeService = appointmentsAddRangeService;
        _appointmentUpdaterService = appointmentUpdaterService;
        _emailSender = emailSender;
        _prescriptionAdderService = prescriptionAdderService;
        _prescriptionGetterService = prescriptionGetterService;
        _appointmentsDeleterService = appointmentsDeleterService;
        _referralsGetterService = referralsGetterService;
        _referralsAdderService = referralsAdderService;
    }

    // GET
    [Authorize(Roles = "Admin, Patient")]
    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1) page = 1;
        int pageSize = 5;

        var data = await _appointmentsGetterService.GetAll();
        var patients = await _patientsGetterService.GetAll();
        ViewBag.Patients = new SelectList(patients, "Id", "FullName");
        if (data != null)
        {
            var paginatedResult = PaginatedGroup<DateTime?, AppointmentResponse>.Create(
                data.Where(model => model.Start.Value.Date >= DateTime.Today),
                x => x.Start.Value.Date,
                page,
                pageSize
            );

            return View(paginatedResult);
        }

        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult CreateAppointment()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateAppointment(AppointmentAddRequest request)
    {
        if (ModelState.IsValid)
        {
            var appointments = await _appointmentsRepository.GetAll();
            if (appointments != null)
            {
                var appointment = request.ToAppointment();
                var overlappingEntry = appointments
                    .Any(app =>
                        (appointment.Start >= app.Start && appointment.Start < app.End) ||
                        (appointment.End > app.Start && appointment.End <= app.End));
                if (overlappingEntry)
                {
                    ModelState.AddModelError("Start", "The appointment on the given date is already in the schedule");
                    return View(request);
                }
            }

            var createdAppointment = await _appointmentsAdderService.Add(request);
            TempData["SuccessMessage"] = "Appointment created.";
            return RedirectToAction(nameof(DashboardController.Index));
        }

        return View(request);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route($"{{{nameof(appointmentId)}}}")]
    public async Task<IActionResult> Show(Guid appointmentId)
    {
        var response = await _appointmentsGetterService.Get(appointmentId);
        if (response is null) return RedirectToAction("Index");
        var patient = await _patientsGetterService.Get(response.PatientId);
        if (patient is null) return RedirectToAction("Index");

        return View(model: new AppointmentDetails() { AppointmentResponse = response, PatientResponse = patient });
    }

    [Authorize(Roles = "Patient")]
    [HttpPost]
    public async Task<IActionResult> ReserveAppointment(Guid appointmentId)
    {
        AppointmentResponse? response = await _appointmentsGetterService.Get(appointmentId);
        if (response is null)
        {
            return RedirectToAction("Index");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Index");
        }

        var patient = await _patientsGetterService.GetByUserId(user.Id);
        if (patient is null)
        {
            return RedirectToAction("Index");
        }

        var request = new AppointmentReserveRequest() { PatientId = patient.Id, Id = appointmentId };
        var reserve = await _appointmentsBookerService.Reserve(request);
        if (reserve.PatientId is not null)
        {
            // DateTime reminderTime = reserve.Start.Value.AddHours(-24);      
            DateTime reminderTime = DateTime.Now.AddSeconds(10);
            string message = $"Your visit will take place in: {reserve.Start.Value.Humanize()} \n Date of the visit: {reserve.Start.Value}";
            BackgroundJob.Schedule(
                () => _emailSender.SendEmailAsync("marcin13101999@gmail.com", "Appointment reminder",
                    message),
                reminderTime);
        }

        if (response.Start != null) TempData["SuccessMessage"] = "Appointment booked - " + response.Start.Value.Date;
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AddRange()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddRange(AppointmentAddRangeRequest request)
    {
        if (ModelState.IsValid)
        {
            var addRange = await _appointmentsAddRangeService.AddRange(request);
            TempData["test"] = addRange.Count;
            return RedirectToAction("Index");
        }

        return View(request);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> StepOne(Guid appointmentId)
    {
        AppointmentResponse? response = await _appointmentsGetterService.Get(appointmentId);
        if (response is null) return RedirectToAction("Index", "Today");
        var patient = await _patientsGetterService.Get(response.PatientId);
        if (response.PatientId != null)
        {
            var finishedAppointments =
                await _appointmentsGetterService.GetAllFinishedPatientsAppointments(response.PatientId.Value);
            foreach (var item in finishedAppointments)
            {
                item.SetPrescriptions(_prescriptionGetterService.GetAllByAppointmentId(item.Id));
                item.SetReferrals(_referralsGetterService.GetAllByAppointmentId(item.Id));
            }

            ViewBag.AllAppointments = finishedAppointments.OrderByDescending(app => app.Start).ToList();
        }

        ViewData["Patient"] = patient;
        ViewData["Appointment"] = response;
        HttpContext.Session.SetObject<PatientResponse>("Patient", patient);
        HttpContext.Session.SetObject<AppointmentResponse>("Appointment", response);
        var updateRequestFromSession = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        if (updateRequestFromSession == null)
        {
            AppointmentUpdateRequest updateRequest = response.ToAppointmentUpdateRequest();
            updateRequest.Prescriptions = new List<PrescriptionAddRequest>();
            updateRequest.Referrals = new List<ReferralAddRequest>();
            HttpContext.Session.SetObject("StepOne", updateRequest);
            return View(updateRequest);
        }

        return View(updateRequestFromSession);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult StepOne(AppointmentUpdateRequest request)
    {
        if (request.Finished is null)
        {
            request.State = State.During;
            request.Finished = false;
        }

        var collection = HttpContext.Session.GetObject<List<PrescriptionAddRequest>>("List");
        var referrals = HttpContext.Session.GetObject<List<ReferralAddRequest>>("ListR");
        if (collection != null)
        {
            request.Prescriptions = collection;
        }

        if (referrals != null)
        {
            request.Referrals = referrals;
        }

        if (ModelState.IsValid)
        {
            //var model = await _appointmentUpdaterService.Update(request: request);
            request.Prescriptions = collection ?? new List<PrescriptionAddRequest>();
            request.Referrals = referrals ?? new List<ReferralAddRequest>();
            HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne", request);
            var model = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
            return RedirectToAction("AddPrescription");
        }

        return View(request);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AddPrescription()
    {
        var model = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        ViewBag.List = model.Prescriptions;
        ViewBag.Appointment = model;
        ViewBag.Patient = HttpContext.Session.GetObject<PatientResponse>("Patient");
        ViewBag.Details = HttpContext.Session.GetObject<AppointmentResponse>("Appointment");
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddPrescription(PrescriptionAddRequest request)
    {
        var appointment = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");

        if (request is { FirstDigit: not null, SecondDigit: not null, ThirdDigit: not null, FourthDigit: not null } &&
            appointment != null)
        {
            request.AppointmentId = appointment.Id;
            appointment?.Prescriptions?.Add(request);
            if (appointment != null)
            {
                HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne", appointment);
                HttpContext.Session.SetObject<List<PrescriptionAddRequest>>("List", appointment.Prescriptions);
            }
        }

        if (request is { FirstDigit: null, SecondDigit: null, ThirdDigit: null, FourthDigit: null, Information: null })
        {
            HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne", appointment);
            var obj = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
            return RedirectToAction("AddReferral", obj);
        }

        return RedirectToAction("AddPrescription");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AddReferral()
    {
        var appointmentModel = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        ViewBag.Referrals = appointmentModel.Referrals;
        ViewBag.Appointment = appointmentModel;
        ViewBag.Patient = HttpContext.Session.GetObject<PatientResponse>("Patient");
        ViewBag.Details = HttpContext.Session.GetObject<AppointmentResponse>("Appointment");
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddReferral(ReferralAddRequest request)
    {
        var appointment = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");

        if (request is { FirstDigit: not null, SecondDigit: not null, ThirdDigit: not null, FourthDigit: not null } &&
            appointment != null)
        {
            request.AppointmentId = appointment.Id;
            appointment?.Referrals?.Add(request);
            if (appointment != null)
            {
                HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne", appointment);
                HttpContext.Session.SetObject<List<ReferralAddRequest>>("ListR", appointment.Referrals);
            }
        }

        if (request is { FirstDigit: null, SecondDigit: null, ThirdDigit: null, FourthDigit: null, Information: null })
        {
            HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne", appointment);
            var obj = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
            return RedirectToAction("Summary", obj);
        }

        return RedirectToAction("AddReferral");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Summary()
    {
        var obj = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        AppointmentResponse? response = await _appointmentsGetterService.Get(obj.Id);
        if (response is null) return RedirectToAction("Index", "Today");
        var patient = await _patientsGetterService.Get(response.PatientId);
        ViewBag.Appointment = response;
        ViewBag.Patient = patient;
        return View(obj);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Save(AppointmentUpdateRequest request)
    {
        if (ModelState.IsValid)
        {
            var a = await _appointmentUpdaterService.Update(request);
            if (request.Prescriptions is not null)
            {
                if (request.Prescriptions.Any())
                {
                    var b = await _prescriptionAdderService.AddRange(request.Prescriptions);
                }
            }

            if (request.Referrals is not null)
            {
                if (request.Referrals.Any())
                {
                    var b = await _referralsAdderService.AddRange(request.Referrals);
                }
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Today");
        }

        return Json(request);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult RemovePrescription(int id)
    {
        if (id < 0)
            return RedirectToAction("AddPrescription");
        var request = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        if (request?.Prescriptions != null && request.Prescriptions.Any())
        {
            request.Prescriptions.RemoveAt(id);
        }

        if (request != null) HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne", request);
        return RedirectToAction("AddPrescription");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult RemoveReferral(int id)
    {
        if (id < 0)
            return RedirectToAction("AddReferral");
        var request = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        if (request?.Referrals != null && request.Referrals.Any())
        {
            request.Referrals.RemoveAt(id);
        }

        if (request != null) HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne", request);
        return RedirectToAction("AddReferral");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> SignInPatient(Guid? patientId, Guid? appointmentId)
    {
        var request = new AppointmentReserveRequest()
        {
            Id = appointmentId,
            PatientId = patientId
        };
        var response = await _appointmentsBookerService.Reserve(request);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin, Patient")]
    [HttpPost]
    public async Task<IActionResult> CancelAppointment(Guid? appointmentId)
    {
        if (appointmentId is null)
            return RedirectToAction("Index");
        await _appointmentsBookerService.CancelReservation(appointmentId.Value);
        if (User.IsInRole("Patient"))
            return RedirectToAction("PatientAppointments");
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> DeleteAppointments(DateTime dateTime)
    {
        var matchingAppointments = await _appointmentsGetterService.GetAllAvalibleByDate(dateTime);
        if (matchingAppointments.Count > 0)
        {
            return View(matchingAppointments.OrderBy(tmp => tmp.Start.Value).ToList());
        }
        
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> DeleteAppointmentsRange(List<Guid> ids)
    {
        var deletedAppointment = await _appointmentsDeleterService.DeleteRange(ids);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> PatientAppointments(int tab = 1)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var patient = await _patientsGetterService.GetByUserId(Guid.Parse(userId));
        var allAppointments = await _appointmentsGetterService.GetAllPatientsAppointments(patient.Id);
        if (tab == 1)
        {
            ViewBag.Tab = 1;
            return View(allAppointments.Where(appointment => appointment is { State: State.Reserved, Finished: null })
                .OrderBy(appointment => appointment.Start).ToList());
        }
        if (tab == 2)
        {
            foreach (var item in allAppointments)
            {
                item.SetPrescriptions(_prescriptionGetterService.GetAllByAppointmentId(item.Id));
                item.SetReferrals(_referralsGetterService.GetAllByAppointmentId(item.Id));
            }
            ViewBag.Tab = 2;
            return View(allAppointments.Where(appointemnt => appointemnt.State == State.Finished)
                .OrderByDescending(appointment => appointment.End).ToList());
        }

        return RedirectToAction("Index", "Dashboard");
    }
}