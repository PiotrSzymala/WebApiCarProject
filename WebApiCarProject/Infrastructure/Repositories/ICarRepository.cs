using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Models.Dtos;

namespace WebApiCarProject.Infrastructure.Repositories;

public interface ICarRepository
{
    Task<Car> GetCarAsync(long id);
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task InsertCarAsync(Car car);
    Task DeleteCarAsync(long carId);
    Task UpdateCarAsync(Car car, CarCreateDto carCreateDto);
    Task SaveAsync();
}