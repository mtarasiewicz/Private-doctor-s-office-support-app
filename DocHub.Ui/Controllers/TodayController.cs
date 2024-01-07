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

    public TodayController(IAppointmentsGetterService appointmentsGetterService)
    {
        _appointmentsGetterService = appointmentsGetterService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var data = await _appointmentsGetterService.GetAllReservedByDate(new DateTime(2024, 01, 06));
        TodayAppointments todayAppointments = new TodayAppointments(data);
        return View(todayAppointments);
    }
}