using Microsoft.AspNetCore.Mvc;

namespace DocHub.Ui.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
