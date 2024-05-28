using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCarProject.Controllers;
using WebApiCarProject.Models;
using WebApiCarProject.Tests.MockingClass;

namespace WebApiCarProject.Tests;

public class AuthControllerTests
{
    private readonly AuthController _controller;
    private readonly AuthServiceMock _stubAuthService;
    private readonly MediatorMock _stubMediator;

    public AuthControllerTests()
    {
        _stubAuthService = new AuthServiceMock();
        _stubMediator = new MediatorMock();

        var httpContext = new DefaultHttpContext();

        _controller = new AuthController(_stubMediator, _stubAuthService)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext }
        };
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenRegistrationIsSuccessful()
    {
        // Arrange
        _stubMediator.CommandResult = true;
        var registerForm = new RegisterForm { Username = "testUser", Password = "testPass" };

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
    {
        // Arrange
        _stubMediator.CommandResult = false;
        var registerForm = new RegisterForm { Username = "testUser", Password = "testPass" };

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenLoginFails()
    {
        // Arrange
        _stubMediator.CommandResult = false;
        var loginForm = new LoginForm { Username = "testUser", Password = "testPass" };

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
            new(ClaimTypes.Name, "testUser")
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