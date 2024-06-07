using Microsoft.EntityFrameworkCore;
using WebApiCarProject.Infrastructure.DatabseContexts;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Models.Dtos;

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

    public async Task UpdateCarAsync(Car car, CarCreateDto carCreateDto)
    {
        var databaseCar = await _context.Cars.FirstOrDefaultAsync(x => x.Id == car.Id);

        if (databaseCar == null)
            throw new Exception("Car does not exist");

        car.Brand = carCreateDto.Brand;
        car.Model = carCreateDto.Model;
        car.Year = carCreateDto.Year;
        car.RegistryPlate = carCreateDto.RegistryPlate;
        car.VinNumber = carCreateDto.VinNumber;
        car.IsAvailable = carCreateDto.IsAvailable;

        _context.Entry(databaseCar).CurrentValues.SetValues(car);
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