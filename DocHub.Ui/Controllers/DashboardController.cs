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
        private readonly IConfiguration _configuration;
        public DashboardController(IPatientsGetterService patientsGetterService, IConfiguration configuration)
        {
            _patientsGetterService = patientsGetterService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var email = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];
            ViewBag.Credentials = email + " " + password;
            return View();
        }
    }
}
