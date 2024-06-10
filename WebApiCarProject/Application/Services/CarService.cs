using Microsoft.AspNetCore.Http.HttpResults;
using System;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Infrastructure.Exceptions;
using WebApiCarProject.Infrastructure.Repositories;
using WebApiCarProject.Models.Dtos;

namespace WebApiCarProject.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IGenericRepository<Car> _carRepository;

        public CarService(IGenericRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();

            if (cars.Count == 0)
                throw new NotFoundException(404, "Cars not Found"); //todo create custom exception not found 

            return cars;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);

            if (car is null)
                throw new NotFoundException(404, "Car not Found");

            return car;
        }

        public async Task CreateCarAsync(Car car)
        {
            var isCarExist = (await _carRepository.GetByIdAsync(car.Id)) != null;

            if (isCarExist)
                throw new Exception("Car Already exists");

            _carRepository.Add(car);
            await _carRepository.SaveAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);

            if (car is null)
            {
                throw new NotFoundException(404, "Car not Found");
            }

            _carRepository.Remove(car);
            await _carRepository.SaveAsync();
        }

        public async Task UpdateCarAsync(int id, CarCreateDto carCreateDto)
        {
            var car = await _carRepository.GetByIdAsync(id);

            if (car is null)
            {
                throw new NotFoundException(404, "Car not Found");
            }

            car.Brand = carCreateDto.Brand;
            car.Model = carCreateDto.Model;
            car.Year = carCreateDto.Year;
            car.RegistryPlate = carCreateDto.RegistryPlate;
            car.VinNumber = carCreateDto.VinNumber;
            car.IsAvailable = carCreateDto.IsAvailable;

            _carRepository.Update(car);
            await _carRepository.SaveAsync();
        }
    }
}
