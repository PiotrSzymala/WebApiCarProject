using System.Security.Claims;
using WebApiCarProject.Application.Services;

namespace WebApiCarProject.Tests.MockingClass;

public class AuthServiceMock : IAuthService
{
    public bool RegisterResult { get; set; }
    public bool LoginResult { get; set; }

    public Task<bool> Register(string username, string password)
    {
        return Task.FromResult(RegisterResult);
    }

    public Task<bool> Login(string username, string password)
    {
        return Task.FromResult(LoginResult);
    }

    public ClaimsIdentity GetClaimsIdentity(string mail)
    {
        return new ClaimsIdentity();
    }
}