using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using DocHub.Ui.Session;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocHub.Ui.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}