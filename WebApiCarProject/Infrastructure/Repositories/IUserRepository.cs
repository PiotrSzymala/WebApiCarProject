using WebApiCar.Infrastructure.Entities;

namespace WebApiCar.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string mail);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
