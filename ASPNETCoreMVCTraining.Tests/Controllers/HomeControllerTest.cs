using ASPNETCoreMVCTraining.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace ASPNETCoreMVCTraining.Tests.Controllers;

[TestFixture]
public class HomeControllerTest
{
    private Mock<ILogger<HomeController>> _loggerMock;
    private HomeController _controller;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new();

        _controller = new HomeController(_loggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _controller?.Dispose();
    }

    [Test]
    public void Index_RequestToPage_ReturnView()
    {
        // Arrange
        // Act
        var actual = _controller.Index();

        // Assert
        Assert.That(actual, Is.Not.Null);
    }
}
