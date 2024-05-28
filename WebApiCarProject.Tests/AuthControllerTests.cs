using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiCarProject.Application.Commands;
using WebApiCarProject.Application.Services;
using WebApiCarProject.Controllers;
using WebApiCarProject.Models;

namespace WebApiCarProject.Tests;

public class AuthControllerTests
{
    private readonly AuthController _controller;
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<IMediator> _mockMediator;

    public AuthControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _mockAuthService = new Mock<IAuthService>();

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
        var registerForm = ArrangeMockedRegisterForm(true);

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _mockMediator.Verify(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    private RegisterForm ArrangeMockedRegisterForm(bool status)
    {
        var registerForm = new RegisterForm { Username = "testUser", Password = "testPass" };
        _mockMediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(status);
        return registerForm;
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
    {
        // Arrange
        var registerForm = ArrangeMockedRegisterForm(false);

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        Assert.IsType<BadRequestResult>(result);
        _mockMediator.Verify(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenLoginFails()
    {
        // Arrange
        var loginForm = new LoginForm { Username = "testUser", Password = "testPass" };
        _mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.Login(loginForm);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void IsAuthenticated_ReturnsOk_WhenUserIsAuthenticated()
    {
        // Arrange
        var mockPrincipal = new Mock<ClaimsPrincipal>();
        mockPrincipal.Setup(p => p.Identity.IsAuthenticated).Returns(true);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = mockPrincipal.Object }
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
        var mockPrincipal = new Mock<ClaimsPrincipal>();
        mockPrincipal.Setup(p => p.Identity.IsAuthenticated).Returns(false);


        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = mockPrincipal.Object }
        };

        // Act & Assert
        var exception = Assert.Throws<ApplicationException>(() => _controller.IsAuthenticated());
        Assert.Equal("Unauthorized", exception.Message);
    }
}