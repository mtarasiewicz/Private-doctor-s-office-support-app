using System.Runtime.InteropServices.JavaScript;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Core.Domain.Models;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.Services;
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

    public AppointmentsController(IAppointmentsGetterService appointmentsGetterService,
        IAppointmentsAdderService appointmentsAdderService, IAppointmentsBookerService appointmentsBookerService,
        IPatientsGetterService patientsGetterService, UserManager<ApplicationUser> userManager,
        IAppointmentsRepository appointmentsRepository, IAppointmentsAddRangeService appointmentsAddRangeService, IAppointmentUpdaterService appointmentUpdaterService)
    {
        _appointmentsGetterService = appointmentsGetterService;
        _appointmentsAdderService = appointmentsAdderService;
        _appointmentsBookerService = appointmentsBookerService;
        _patientsGetterService = patientsGetterService;
        _userManager = userManager;
        _appointmentsRepository = appointmentsRepository;
        _appointmentsAddRangeService = appointmentsAddRangeService;
        _appointmentUpdaterService = appointmentUpdaterService;
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
        if (response is null) return RedirectToAction("Index");

        var user = await _userManager.GetUserAsync(User);
        if (user is null) return RedirectToAction("Index");

        var patient = await _patientsGetterService.GetByUserId(user.Id);
        if (patient is null) return RedirectToAction("Index");
        var request = new AppointmentReserveRequest() { PatientId = patient.Id, Id = appointmentId };
        var reserve = await _appointmentsBookerService.Reserve(request);
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
    public async Task<IActionResult> Update(Guid appointmentId)
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


        ViewData["Patient"] = patient;
        ViewData["Appointment"] = response;
        AppointmentUpdateRequest updateRequest = response.ToAppointmentUpdateRequest();
        return View(updateRequest);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Update(AppointmentUpdateRequest request)
    {
        if (request.Finished is null)
        {
            request.State = State.During;
            request.Finished = false;
        }
        if (ModelState.IsValid)
        {
            var model = await _appointmentUpdaterService.Update(request: request);
            return RedirectToAction("Index", "Today");
        }

        return View(request);
    }
}