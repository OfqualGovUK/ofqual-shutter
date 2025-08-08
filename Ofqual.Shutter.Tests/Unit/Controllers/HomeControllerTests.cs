using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Ofqual.Shutter.Web.Controllers;
using Ofqual.Shutter.Web.Models;

namespace Ofqual.Shutter.Web.Tests.Unit.Controllers;

public class HomeControllerTests
{
    /// <summary>
    /// Tests that the Index action returns a ViewResult with the correct model when all configuration values are provided.
    /// </summary>
    [Fact]
    [Trait("Category", "Unit")]
    public void Index_ReturnsViewResult()
    {
        //Arrange
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["ShutterPage:Service_Name"]).Returns("Test Service");
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

    /// <summary>
    /// Tests that the Index action throws an ArgumentNullException when a required configuration key is missing.
    /// </summary>
    /// <param name="missingKey"></param>
    [Theory]
    [InlineData("ShutterPage:Service_Name")]
    [InlineData("ShutterPage:Contact_Info")]
    [InlineData("ShutterPage:Contact_Url")]
    [InlineData("ShutterPage:Contact_Url_Text")]
    public void Index_ThrowsArgumentNullException_WhenConfigMissing(string missingKey)
    {
        // Arrange
        var configMock = new Mock<IConfiguration>();

        configMock.Setup(c => c["ShutterPage:Service_Name"]).Returns(missingKey == "ShutterPage:Service_Name" ? null : "Test Service");
        // Setup all keys except Downtime as it is allowd to be null
        configMock.Setup(c => c["ShutterPage:Downtime"]).Returns("2025-11-19T09:00:00");
        configMock.Setup(c => c["ShutterPage:Contact_Info"]).Returns(missingKey == "ShutterPage:Contact_Info" ? null : "Test Contact Info");
        configMock.Setup(c => c["ShutterPage:Contact_Url"]).Returns(missingKey == "ShutterPage:Contact_Url" ? null : "https://recognition-service-test.com");
        configMock.Setup(c => c["ShutterPage:Contact_Url_Text"]).Returns(missingKey == "ShutterPage:Contact_Url_Text" ? null : "Contact Us");

        var controller = new HomeController(configMock.Object);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => controller.Index());
    }

    /// <summary>
    /// Tests that the Index action uses null for Downtime when an invalid date format is provided in the configuration.
    /// </summary>
    [Fact]
    [Trait("Category", "Unit")]
    public void Index_InvalidDowntime_UseNullInstead()
    {
        // Arrange: The downtime is set to an invalid date
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["ShutterPage:Service_Name"]).Returns("Test Service");
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