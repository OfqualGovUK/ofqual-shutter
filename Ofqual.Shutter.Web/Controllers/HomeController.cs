using Elastic.CommonSchema;
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
            ServiceName = _config["ShutterPage:Service_Name"] ?? throw new ArgumentNullException("ShutterPage:Service_Name", "Service name is not configured."),
            Downtime = DateTime.TryParse(_config["ShutterPage:Downtime"], out var downtime) ? downtime : null,
            ContactInfo = _config["ShutterPage:Contact_Info"] ?? throw new ArgumentNullException("ShutterPage:Contact_Info", "Contact information is not configured."),
            ContactUrl = _config["ShutterPage:Contact_Url"] ?? throw new ArgumentNullException("ShutterPage:Contact_Url", "Contact URL is not configured."),
            ContactUrlText = _config["ShutterPage:Contact_Url_Text"] ?? throw new ArgumentNullException("ShutterPage:Contact_Url_Text", "Contact URL text is not configured.")
        };

        return View(shutterPageConfigurationViewModel);
    }
}

