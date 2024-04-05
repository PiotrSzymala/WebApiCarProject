using Microsoft.EntityFrameworkCore;
using WebApiCar.Infrastructure.DatabseContexts;
using WebApiCar.Infrastructure.Entities;

namespace WebApiCar.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly CarDbContext _context;
        public UserRepository(CarDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
