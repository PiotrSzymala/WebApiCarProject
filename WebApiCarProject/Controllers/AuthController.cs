using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using MediatR;
using System.Net;
using WebApiCar.Application.Commands;
using WebApiCar.Application.Services;
using WebApiCar.Models;


namespace RACH.FrontendApi.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public AuthController(IMediator mediator, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterForm registerForm)
    {
        RegisterCommand command = new(registerForm);

        bool result = await _mediator.Send(command);

        return result ? Ok(result) : BadRequest();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)
    {
        LoginCommand command = new(loginForm);

        bool result = await _mediator.Send(command);

        if (result)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(_authService.GetClaimsIdentity(loginForm.Username)));

            return Ok(result);
        }

        return BadRequest();
    }

    [HttpGet("IsAuthenticated")]
    [Authorize]
    public IActionResult IsAuthenticated()
    {
        if (User.Identity.IsAuthenticated) return Ok();

        throw new ApplicationException("Unauthorized");
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}
