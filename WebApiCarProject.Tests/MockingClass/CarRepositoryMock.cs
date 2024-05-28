using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Infrastructure.Repositories;

namespace WebApiCarProject.Tests.MockingClass;

public class CarRepositoryMock : ICarRepository
{
    private readonly List<Car> _cars = new();

    public Task<Car> GetCarAsync(long id)
    {
        if (id != 1)
            return Task.FromResult<Car>(null);

        return Task.FromResult(new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 });
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return new List<Car>
        {
            new() { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
            new() { Id = 2, Brand = "Honda", Model = "Civic", Year = 2019 }
        };
    }

    public Task InsertCarAsync(Car car)
    {
        _cars.Add(car);
        return Task.CompletedTask;
    }

    public Task DeleteCarAsync(long id)
    {
        var car = _cars.Find(c => c.Id == id);
        if (car != null) _cars.Remove(car);
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        return Task.CompletedTask;
    }
}