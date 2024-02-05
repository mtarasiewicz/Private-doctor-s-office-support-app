using DocHub.Core.Domain.Models;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace DocHub.Ui.Controllers;
[Route("[controller]/[action]")]
public class TodayController : Controller
{
    private readonly IAppointmentsGetterService _appointmentsGetterService;
    private readonly IAppointmentsBookerService _appointmentsBookerService;
    private readonly IAppointmentsDeleterService _appointmentsDeleterService;

    public TodayController(IAppointmentsGetterService appointmentsGetterService, IAppointmentsBookerService appointmentsBookerService, IAppointmentsDeleterService appointmentsDeleterService)
    {
        _appointmentsGetterService = appointmentsGetterService;
        _appointmentsBookerService = appointmentsBookerService;
        _appointmentsDeleterService = appointmentsDeleterService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        HttpContext.Session.Clear();
        var data = await _appointmentsGetterService.GetAllReservedByDate(DateTime.Today);
        TodayAppointments todayAppointments = new TodayAppointments(data);
        return View(todayAppointments);
    }

    [HttpPost]
    public async Task<IActionResult> CancelAppointment(Guid appointmentId, bool beDeleted = false)
    {
        var matchingAppointment = await _appointmentsGetterService.Get(appointmentId);
        if (beDeleted)
        {
            var deleteAppointment = await _appointmentsDeleterService.Delete(appointmentId);
            return RedirectToAction("Index");
        }

        var unbookedAppointment = await _appointmentsBookerService.CancelReservation(appointmentId);
        return RedirectToAction("Index");
    }
}