using Microsoft.AspNetCore.Mvc;
using Ofqual.Shutter.Web.Controllers;

namespace Ofqual.Shutter.Web.Tests.Unit.Controllers;

public class HomeControllerTests
{
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        _controller = new HomeController();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Index_ReturnsViewResult()
    {
        // Act
        var result = _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }
}