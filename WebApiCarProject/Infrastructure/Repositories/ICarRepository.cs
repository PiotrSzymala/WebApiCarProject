using WebApiCar.Infrastructure.Entities;

namespace WebApiCar.Infrastructure.Repositories
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
