using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreMVCTraining.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
