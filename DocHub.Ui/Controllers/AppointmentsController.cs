using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using DocHub.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DocHub.Ui.Controllers;
[Route("[controller]/[action]")]
public class AppointmentsController : Controller
{
    private readonly IAppointmentsGetterService _appointmentsGetterService;
    private readonly IAppointmentsAdderService _appointmentsAdderService;
    private readonly IAppointmentsBookerService _appointmentsBookerService;
    private readonly IPatientsGetterService _patientsGetterService;
    private readonly UserManager<ApplicationUser> _userManager;
    private const int PageSize = 5;
    public AppointmentsController(IAppointmentsGetterService appointmentsGetterService, IAppointmentsAdderService appointmentsAdderService, IAppointmentsBookerService appointmentsBookerService, IPatientsGetterService patientsGetterService, UserManager<ApplicationUser> userManager)
    {
        _appointmentsGetterService = appointmentsGetterService;
        _appointmentsAdderService = appointmentsAdderService;
        _appointmentsBookerService = appointmentsBookerService;
        _patientsGetterService = patientsGetterService;
        _userManager = userManager;
    }
    // GET
    public async Task<IActionResult> Index(int page = 1)
    {
        var appointments = await _appointmentsGetterService.GetAll();
        if (appointments is null) return View();
        var groupedAppointments = appointments.GroupBy(a => a.Start.Value.Date).OrderBy(a => a.Key);
     
        return View(groupedAppointments);
    }

    [HttpGet]
    public IActionResult CreateAppointment()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateAppointment(AppointmentAddRequest request)
    {
        if (!ModelState.IsValid) return View(request);
        var createdAppointment = await _appointmentsAdderService.Add(request);
        TempData["SuccessMessage"] = "Appointment created.";
        return RedirectToAction(nameof(DashboardController.Index));

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
    public async Task<IActionResult> ReserveAppointment(AppointmentReserveRequest request)
    {
        AppointmentResponse? response = await _appointmentsGetterService.Get(request.Id);
        if (response is null) return RedirectToAction("Index");
        ApplicationUser? user = await _userManager.GetUserAsync(User);
        if (user is null) return RedirectToAction("Index");
       PatientResponse? patient  = await  _patientsGetterService.GetByUserId(user.Id);
       if (patient is null) return RedirectToAction("Index");
       request.PatientId = patient.Id;
       AppointmentResponse reservedAppointment = await _appointmentsBookerService.Reserve(request);
       return RedirectToAction("Index");
    }

    private IEnumerable<IGrouping<DateTime, AppointmentResponse>> Paginate(
        IOrderedEnumerable<IGrouping<DateTime, AppointmentResponse>> appointments, int page)
    {
        return appointments.Skip((page - 1) * PageSize).Take(PageSize);
    }
}