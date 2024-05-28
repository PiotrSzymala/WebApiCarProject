using WebApiCarProject.Infrastructure.Entities;

namespace WebApiCarProject.Infrastructure.Repositories;

public interface ICarRepository
{
    Task<Car> GetCarAsync(long id);
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task InsertCarAsync(Car car);
    Task DeleteCarAsync(long carId);
    Task SaveAsync();
}