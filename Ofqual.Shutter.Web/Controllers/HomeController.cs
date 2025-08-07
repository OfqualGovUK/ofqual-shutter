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
            ServiceName = _config["ShutterPage:SERVICE_NAME"] ?? "Ofqual Recognition",
            Downtime = DateTime.TryParse(_config["ShutterPage:DOWNTIME"], out var downtime) ? downtime : null,
            ContactInfo = _config["ShutterPage:CONTACT_INFO"] ?? "Contact Ofqual Support.",
            ContactUrl = _config["ShutterPage:CONTACT_URL"] ?? "https://www.ofqual.gov.uk/contact-us/",
            ContactUrlText = _config["ShutterPage:CONTACT_URL_TEXT"] ?? "Contact Ofqual Support"
        };

        return View(shutterPageConfigurationViewModel);
    }
}

