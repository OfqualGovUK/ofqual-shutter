namespace Ofqual.Shutter.Web.Models;

public class HoldingPageConfigurationViewModel
{
    // Service Information
    public required string ServiceName { get; set; }
    // When it's back online
    public DateTime? Downtime { get; set; }
    public required string ContactInfo { get; set; }
    public required string ContactUrl { get; set; }
    public string ContactUrlText { get; set; } = "Contact Ofqual Support";
}

