using ASPNETCoreMVCTraining.Interfaces;
using ASPNETCoreMVCTraining.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNETCoreMVCTraining.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;

        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;

        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;

        public HomeController(ILogger<HomeController> logger,
                          ISingletonService singletonService1,
                          ISingletonService singletonService2,
                          ITransientService transientService1,
                          ITransientService transientService2,
                          IScopedService scopedService1,
                          IScopedService scopedService2)
        {
            _logger = logger;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
        }

        [Route("")]
        [Route("{id}")]
        public IActionResult Index(string? id)
        {
            ViewBag.Message1 = $"First transientI instance {_transientService1.GetId()}";
            ViewBag.Message2 = $"Second transient Instance {_transientService2.GetId()}";


            ViewBag.Message3 = $"First scoped Instance {_scopedService1.GetId()}";
            ViewBag.Message4 = $"Second scoped Instance {_scopedService2.GetId()}";

            ViewBag.Message5 = $"First singleton Instance { _singletonService1.GetId()}";
            ViewBag.Message6 = $"Second singleton Instance { _singletonService2.GetId()}";

            ViewBag.Message7 = $"RequestId: {id}";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
