using System.Security.Claims;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WebApiCarProject.Application.Commands;
using WebApiCarProject.Application.Services;
using WebApiCarProject.Controllers;
using WebApiCarProject.Models;


namespace WebApiCarProject.Tests;

public class AuthControllerTests
{
    private readonly Mock<IMediator> _mockMediator = new();
    private readonly Mock<IAuthService> _mockAuthService = new();
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _controller = new AuthController(_mockMediator.Object, _mockAuthService.Object);
    }

    [Fact]
    public async Task Register_WhenSuccessful_ReturnsOk()
    {
        // Arrange
        var registerForm = new RegisterForm { /* properties set up */ };
        _mockMediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Register_WhenFailed_ReturnsBadRequest()
    {
        // Arrange
        var registerForm = new RegisterForm { /* properties set up */ };
        _mockMediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Register(registerForm);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    

    [Fact]
    public async Task Login_WhenFailed_ReturnsBadRequest()
    {
        // Arrange
        var loginForm = new LoginForm { /* setup properties */ };
        _mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Login(loginForm);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public void IsAuthenticated_ReturnsOk_WhenUserIsAuthenticated()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "username")
        }, "TestAuthentication"));

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.User).Returns(claimsPrincipal);

        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContextMock.Object
        };

        _controller.ControllerContext = controllerContext;
    }
  
}