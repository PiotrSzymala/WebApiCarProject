﻿using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCarProject.Application.Commands;
using WebApiCarProject.Application.Services;
using WebApiCarProject.Models;

namespace WebApiCarProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterForm registerForm)
    {
        RegisterCommand command = new(registerForm);

        var result = await _mediator.Send(command);

        return result ? Ok(result) : BadRequest();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)
    {
        LoginCommand command = new(loginForm);

        var result = await _mediator.Send(command);

        if (result)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(_authService.GetClaimsIdentity(loginForm.Username)));

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