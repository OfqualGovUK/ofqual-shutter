using Microsoft.AspNetCore.Mvc;

namespace Ofqual.Shutter.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

