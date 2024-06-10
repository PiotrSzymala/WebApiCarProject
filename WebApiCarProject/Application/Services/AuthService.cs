using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApiCarProject.Infrastructure.DatabseContexts;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Infrastructure.Repositories;

namespace WebApiCarProject.Application.Services;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<User> _userRepository;

    public AuthService( IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Register(string username, string password)
    {
        User user = new()
        {
            Username = username,
            PasswordHash = HashPassword(password)
        };

        var isUserAlreadyExist = (await _userRepository.SelectAsync(x => x.Username == user.Username)) != null;

        if (isUserAlreadyExist)
            throw new Exception("User already created.");

        _userRepository.Add(user);

        var userRegisteredSuccessfully = await _userRepository.SaveAsync() > 0;

        if (!userRegisteredSuccessfully) 
            throw new ApplicationException("Error while saving user to database");

        return true;
    }

    public async Task<bool> Login(string username, string password)
    {
        var correctCredentials = (await _userRepository.GetAllAsync())
            .SingleOrDefault(u => u.Username == username && u.PasswordHash == HashPassword(password)) != null;

        if (!correctCredentials)
            throw new Exception("Could not find User by given credentials. Enter correct Username and Password.");
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