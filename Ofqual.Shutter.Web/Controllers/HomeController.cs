using Microsoft.AspNetCore.Mvc;
using Ofqual.Shutter.Web.Models;

namespace Ofqual.Shutter.Web.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config; 
    }

    public IActionResult Index()
    {
        var shutterPageConfigurationViewModel = new ShutterPageConfigurationViewModel
        {
            ServiceName = _config["ShutterPage:Sservice_Name"] ?? "Ofqual Recognition",
            Downtime = DateTime.TryParse(_config["ShutterPage:Downtime"], out var downtime) ? downtime : null,
            ContactInfo = _config["ShutterPage:Contact_Info"] ?? "if you require urgent assistance please contact the helpline.",
            ContactUrl = _config["ShutterPage:Contact_Url"] ?? "https://www.gov.uk/guidance/contact-ofqual",
            ContactUrlText = _config["ShutterPage:Contact_Url_Text"] ?? "Contact Ofqual Support"
        };

        return View(shutterPageConfigurationViewModel);
    }
}

