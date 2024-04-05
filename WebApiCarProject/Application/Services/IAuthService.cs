using System.Security.Claims;

namespace WebApiCar.Application.Services
{
    public interface IAuthService
    {
        Task<bool> Register(string username, string password);
        Task<bool> Login(string username, string password);
        ClaimsIdentity GetClaimsIdentity(string mail);
    }
}
