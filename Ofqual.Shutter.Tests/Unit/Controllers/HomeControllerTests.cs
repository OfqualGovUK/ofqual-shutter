using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Ofqual.Shutter.Web.Controllers;
using Ofqual.Shutter.Web.Models;

namespace Ofqual.Shutter.Web.Tests.Unit.Controllers;

public class HomeControllerTests
{

    [Fact]
    [Trait("Category", "Unit")]
    public void Index_ReturnsViewResult()
    {
        //Arrange
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["ShutterPage:Sservice_Name"]).Returns("Test Service");
        configMock.Setup(c => c["ShutterPage:Downtime"]).Returns("2025-11-19T09:00:00");
        configMock.Setup(c => c["ShutterPage:Contact_Info"]).Returns("Test Contact Info");
        configMock.Setup(c => c["ShutterPage:Contact_Url"]).Returns("https://recognition-service-test.com");
        configMock.Setup(c => c["ShutterPage:Contact_Url_Text"]).Returns("Contact Us");

        var controller = new HomeController(configMock.Object);

        // Act
        var result = controller.Index() as ViewResult;

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ShutterPageConfigurationViewModel>(result.Model);
        Assert.Equal("Test Service", model.ServiceName);
        Assert.Equal(new DateTime(2025, 11, 19, 9, 0, 0), model.Downtime);
        Assert.Equal("Test Contact Info", model.ContactInfo);
        Assert.Equal("https://recognition-service-test.com", model.ContactUrl);
        Assert.Equal("Contact Us", model.ContactUrlText);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Index_MissingConfig_UsesDefaultValues()
    {
        // Arrange: The config values aren't set
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["ShutterPage:Sservice_Name"]).Returns((string)null);
        configMock.Setup(c => c["ShutterPage:Downtime"]).Returns((string)null);
        configMock.Setup(c => c["ShutterPage:Contact_Info"]).Returns((string)null);
        configMock.Setup(c => c["ShutterPage:Contact_Url"]).Returns((string)null);
        configMock.Setup(c => c["ShutterPage:Contact_Url_Text"]).Returns((string)null);

        var controller = new HomeController(configMock.Object);

        // Act
        var result = controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ShutterPageConfigurationViewModel>(result.Model);
        Assert.Equal("Ofqual Recognition", model.ServiceName);
        Assert.Null(model.Downtime);
        Assert.Equal("Contact Ofqual Support.", model.ContactInfo);
        Assert.Equal("https://www.ofqual.gov.uk/contact-us/", model.ContactUrl);
        Assert.Equal("Contact Ofqual Support", model.ContactUrlText);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Index_InvalidDowntime_UseNullInstead()
    {
        // Arrange: The downtime is set to an invalid date
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["ShutterPage:Sservice_Name"]).Returns("Test Service");
        // Setting an invalid date format
        configMock.Setup(c => c["ShutterPage:Downtime"]).Returns("InvalidDate");
        configMock.Setup(c => c["ShutterPage:Contact_Info"]).Returns("Test Contact Info");
        configMock.Setup(c => c["ShutterPage:Contact_Url"]).Returns("https://recognition-service-test.com");
        configMock.Setup(c => c["ShutterPage:Contact_Url_Text"]).Returns("Contact Us");


        var controller = new HomeController(configMock.Object);

        // Act
        var result = controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ShutterPageConfigurationViewModel>(result.Model);
        Assert.Equal("Test Service", model.ServiceName);
        Assert.Null(model.Downtime); // Should be null due to invalid date
        Assert.Equal("Test Contact Info", model.ContactInfo);
        Assert.Equal("https://recognition-service-test.com", model.ContactUrl);
        Assert.Equal("Contact Us", model.ContactUrlText);
    }
}