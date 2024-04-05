using Microsoft.EntityFrameworkCore;
using WebApiCar.Infrastructure.DatabseContexts;
using WebApiCar.Infrastructure.Entities;

namespace WebApiCar.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository, IDisposable
    {
        private readonly CarDbContext _context;

        public CarRepository(CarDbContext context)
        {
            _context = context;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }
        public void InsertCar(Car car)
        {
            _context.Cars.Add(car);
        }
        public async Task InsertCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
        }
        public async Task DeleteCarAsync(int carId)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == carId);
            var result = _context.Cars.Remove(car);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
    }
}
