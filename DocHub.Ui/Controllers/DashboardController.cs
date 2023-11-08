using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocHub.Ui.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IPatientsGetterService _patientsGetterService;
        public DashboardController(IPatientsGetterService patientsGetterService)
        {
            _patientsGetterService = patientsGetterService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
