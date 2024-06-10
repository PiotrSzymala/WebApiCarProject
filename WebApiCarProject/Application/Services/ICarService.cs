using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Models.Dtos;

namespace WebApiCarProject.Application.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetCarAsync(int id);
        Task CreateCarAsync(Car car);
        Task DeleteCarAsync(int id);
        Task UpdateCarAsync(int id, CarCreateDto carCreateDto);
    }
}