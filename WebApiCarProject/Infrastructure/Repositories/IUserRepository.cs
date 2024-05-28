using WebApiCarProject.Infrastructure.Entities;

namespace WebApiCarProject.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User> GetUserAsync(string mail);
    Task<IEnumerable<User>> GetAllUsersAsync();
}