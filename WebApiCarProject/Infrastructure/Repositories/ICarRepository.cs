using WebApiCarProject.Infrastructure.Entities;

namespace WebApiCarProject.Infrastructure.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetCarAsync(int id);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        void InsertCar(Car car);
        Task InsertCarAsync(Car car);
        Task DeleteCarAsync(int carId);
        void Save();
        Task SaveAsync();
    }
}
