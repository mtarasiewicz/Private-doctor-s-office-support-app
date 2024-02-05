using Microsoft.AspNetCore.Mvc;

namespace DocHub.Ui.Controllers;

public class Error : Controller
{
    // GET
    [Route($"/{nameof(Error)}")]
    public IActionResult Index()
    {
        return View();
    }
}