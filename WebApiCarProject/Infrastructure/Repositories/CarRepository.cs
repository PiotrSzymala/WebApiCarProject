using Microsoft.EntityFrameworkCore;
using WebApiCarProject.Infrastructure.DatabseContexts;
using WebApiCarProject.Infrastructure.Entities;

namespace WebApiCarProject.Infrastructure.Repositories;

public class CarRepository : ICarRepository, IDisposable
{
    private readonly CarDbContext _context;

    private bool disposed;

    public CarRepository(CarDbContext context)
    {
        _context = context;
    }

    public async Task<Car> GetCarAsync(long id)
    {
        return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task InsertCarAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
    }

    public async Task DeleteCarAsync(long carId)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == carId);
        var result = _context.Cars.Remove(car);
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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                _context.Dispose();
        disposed = true;
    }
}