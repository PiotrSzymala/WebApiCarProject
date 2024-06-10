using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiCarProject.Controllers;
using WebApiCarProject.Application.Commands;
using WebApiCarProject.Application.Services;
using WebApiCarProject.Models;
using Xunit;
using MediatR;

namespace WebApiCarProject.Tests;

public class AuthControllerTests
{
    private const string TestUsr = "testUsr";
    private const string TestPswd = "testPswd";
    private readonly AuthController _controller;
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<IMediator> _mockMediator;

    public AuthControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockMediator = new Mock<IMediator>();

        var httpContext = new DefaultHttpContext();

        _controller = new AuthController(_mockMediator.Object, _mockAuthService.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext }
        };
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenRegistrationIsSuccessful()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true);
        var registerForm = new RegisterForm { Login = TestUsr, Passwd = TestPswd };

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)actionResult.Value);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(false);
        var registerForm = new RegisterForm { Login = TestUsr, Passwd = TestPswd };

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, response.StatusCode);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenLoginFails()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(false);
        var loginForm = new LoginForm { Login = TestUsr, Passwd = TestPswd };

        // Act
        var result = await _controller.Login(loginForm);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void IsAuthenticated_ReturnsOk_WhenUserIsAuthenticated()
    {
        // Arrange
        var mockPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Name, TestUsr)
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = mockPrincipal }
        };

        // Act
        var result = _controller.IsAuthenticated();

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void IsAuthenticated_ThrowsException_WhenUserIsNotAuthenticated()
    {
        // Arrange
        var mockPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = mockPrincipal }
        };

        // Act & Assert
        var exception = Assert.Throws<ApplicationException>(() => _controller.IsAuthenticated());
        Assert.Equal("Unauthorized", exception.Message);
    }
}
