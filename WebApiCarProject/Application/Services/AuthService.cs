using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApiCar.Infrastructure.DatabseContexts;
using WebApiCar.Infrastructure.Entities;

namespace WebApiCar.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly CarDbContext _context;

        public AuthService(CarDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Register(string username, string password)
        {
            User user = new()
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };
            await _context.Users.AddAsync(user);

            bool userRegisteredSuccessfully = (await _context.SaveChangesAsync()) > 0;

            if (!userRegisteredSuccessfully) throw new ApplicationException("Error while saving user to database");

            return true;
        }

        public async Task<bool> Login(string username, string password)
        {
            bool correctCredentials = (await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username && u.PasswordHash == HashPassword(password))) != null;

            if (!correctCredentials)
            {
                throw new Exception("Could not find User by given credentials. Enter correct Username and Password.");
            }
            return correctCredentials;
        }

        public ClaimsIdentity GetClaimsIdentity(string login)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, login)
            };

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return claimsIdentity;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
