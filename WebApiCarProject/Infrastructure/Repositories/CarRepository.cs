using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApiCar.Migrations;
using WebApiCarProject.Infrastructure.DatabseContexts;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Models.Dtos;

namespace WebApiCarProject.Infrastructure.Repositories;

public class CarRepository : IGenericRepository<Car>, IDisposable
{
    private readonly CarDbContext _context;

    private bool disposed;

    public CarRepository(CarDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Car> GetAll()
    {
        return _context.Cars.ToList();
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public Car GetById(int id)
    {
        return _context.Cars.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Car> GetByIdAsync(int id)
    {
        return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Remove(Car sender)
    {
        if (_context.Entry(sender).State == EntityState.Detached)
            _context.Cars.Attach(sender);
        
        _context.Cars.Remove(sender);
    }

    public void Add(in Car sender)
    {
        _context.Add(sender).State = EntityState.Added;
    }

    public void Update(in Car sender)
    {
        _context.Entry(sender).State = EntityState.Modified;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    Task<int> IGenericRepository<Car>.SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public Car Select(Expression<Func<Car, bool>> predicate)
    {
        return _context.Cars
            .Where(predicate).FirstOrDefault()!;
    }

    public async Task<Car> SelectAsync(Expression<Func<Car, bool>> predicate)
    {
        return (await _context.Cars
                .Where(predicate).FirstOrDefaultAsync())!;
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