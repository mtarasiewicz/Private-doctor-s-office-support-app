using System.Net.Mail;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Core.Domain.Models;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.Services;
using DocHub.Ui.Session;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

    public AppointmentsController(IAppointmentsGetterService appointmentsGetterService,
        IAppointmentsAdderService appointmentsAdderService, IAppointmentsBookerService appointmentsBookerService,
        IPatientsGetterService patientsGetterService, UserManager<ApplicationUser> userManager,
        IAppointmentsRepository appointmentsRepository, IAppointmentsAddRangeService appointmentsAddRangeService,
        IAppointmentUpdaterService appointmentUpdaterService, IEmailSenderService emailSender, IPrescriptionAdderService prescriptionAdderService)
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
    }

    // GET
    [Authorize(Roles = "Admin, Patient")]
    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1) page = 1;
        int pageSize = 5;

        var data = await _appointmentsGetterService.GetAll();

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

    [Authorize(Roles = "Patient")]
    [HttpGet]
    [Route($"{{{nameof(appointmentId)}}}")]
    public async Task<IActionResult> ShowAppointment(Guid appointmentId)
    {
        var response = await _appointmentsGetterService.Get(appointmentId);
        if (response is null) return RedirectToAction("Index");

        var request = response.ToAppointmentReserveRequest();
        return View(model: request);
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
            BackgroundJob.Schedule(() => _emailSender.SendEmailAsync("marcin13101999@gmail.com", "Test", $"{reserve.Start.Value}"),
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
            ViewBag.AllAppointments = finishedAppointments.OrderByDescending(app => app.Start).ToList();
        }

       //var model = HttpContext.Session.GetObject<TestModel>("TestModel") ?? new TestModel();
        ViewData["Patient"] = patient;
        ViewData["Appointment"] = response;
        var key = "StepOne" + appointmentId;
        var updateRequestFromSession = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        if (updateRequestFromSession == null)
        {
            AppointmentUpdateRequest updateRequest = response.ToAppointmentUpdateRequest();
            updateRequest.Prescriptions = new List<PrescriptionAddRequest>();
            HttpContext.Session.SetObject("StepOne", updateRequest);
            return View(updateRequest);
        }

        return View(updateRequestFromSession);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult StepOne(AppointmentUpdateRequest request)
    {
        var key = "StepOne" + request.Id;
        var listKey = "List" + request.Id;
        if (request.Finished is null)
        {
            request.State = State.During;
            request.Finished = false;
        }
        var collection = HttpContext.Session.GetObject<List<PrescriptionAddRequest>>("List");
        if (collection != null)
        {
            request.Prescriptions = collection;   
        }
        if (ModelState.IsValid)
        {
            //var model = await _appointmentUpdaterService.Update(request: request);
            request.Prescriptions = collection ?? new List<PrescriptionAddRequest>();
            HttpContext.Session.SetObject<AppointmentUpdateRequest>("StepOne",request);
            var model = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
            return RedirectToAction("AddPrescription");
        }

        return View(request);
    }
    [HttpGet]
    public IActionResult AddPrescription()
    {
        var model = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        ViewBag.List = model.Prescriptions;
        ViewBag.Appointment = model;
        return View();
    }

    [HttpPost]
    public IActionResult AddPrescription(PrescriptionAddRequest request)
    {
        var appointment = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        
        if (request is { FirstDigit: not null, SecondDigit: not null, ThirdDigit: not null, FourthDigit: not null } && appointment != null)
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
            return RedirectToAction("Summary", obj);
        }

        return RedirectToAction("AddPrescription");
    }

    [HttpGet]
    public IActionResult Summary()
    {  
        var obj = HttpContext.Session.GetObject<AppointmentUpdateRequest>("StepOne");
        return View(obj);
    }

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
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Today");
        }

        return Json(request);
    }

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
}