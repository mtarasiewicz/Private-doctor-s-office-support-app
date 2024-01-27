using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using DocHub.Ui.Session;
using Hangfire;
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
            return View();
        }
        //
        // [HttpGet]
        // public IActionResult StepOne()
        // {
        //     var model = HttpContext.Session.GetObject<TestModel>("TestModel") ?? new TestModel();
        //     return View(model);
        // }
        //
        // [HttpPost]
        // public IActionResult StepOne(TestModel model)
        // {
        //     var collection = HttpContext.Session.GetObject<List<TestModel2>>("List");
        //     if (collection != null)
        //     {
        //         model.Models2 = collection;   
        //     }
        //     HttpContext.Session.SetObject<TestModel>("TestModel", model);
        //     return RedirectToAction("StepTwo");
        // }
        //
        // [HttpGet]
        // public IActionResult StepTwo()
        // {
        //     var model = HttpContext.Session.GetObject<TestModel>("TestModel");
        //     ViewBag.List = model.Models2;
        //     return View();
        // }
        //
        // [HttpPost]
        // public IActionResult StepTwo(TestModel2 model)
        // {
        //     var firstModel = HttpContext.Session.GetObject<TestModel>("TestModel");
        //
        //     if (model is { B1: not null, B2: not null })
        //     {
        //         firstModel.Models2.Add(model);
        //         HttpContext.Session.SetObject<TestModel>("TestModel", firstModel);
        //         HttpContext.Session.SetObject("List", firstModel.Models2);
        //     }
        //     
        //
        //     if (model.IsEnd || model is {B1: null, B2: null, IsEnd: false})
        //     {
        //         HttpContext.Session.SetObject<TestModel>("TestModel", firstModel);
        //         var completeModel = HttpContext.Session.GetObject<TestModel>("TestModel");
        //         return View("End", completeModel);
        //     }
        //     return RedirectToAction("StepTwo");
        // }
    }
}