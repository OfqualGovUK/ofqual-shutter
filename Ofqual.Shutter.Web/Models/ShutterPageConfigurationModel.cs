namespace Ofqual.Shutter.Web.Models;

public class ShutterPageConfigurationModel
{
    // Service Information
    public required string ServiceName { get; set; }
    // When it's back online
    public DateTime? Downtime { get; set; }
    public required string ContactInfo { get; set; }
    public required string ContactUrl { get; set; }
    public required string ContactUrlText { get; set; }
}

