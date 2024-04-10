using WebApiCarProject.Infrastructure.Entities;

namespace WebApiCarProject.Infrastructure.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetCarAsync(long id);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        void InsertCar(Car car);
        Task InsertCarAsync(Car car);
        Task DeleteCarAsync(long carId);
        void Save();
        Task SaveAsync();
    }
}
