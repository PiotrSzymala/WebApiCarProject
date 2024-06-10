using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiCarProject.Controllers;
using WebApiCarProject.Application.Services;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Models.Dtos;
using Xunit;
using WebApiCarProject.Infrastructure.Exceptions;
using System.Net;

namespace WebApiCarProject.Tests;

public class CarsControllerTests
{
    private readonly Mock<ICarService> _mockService;
    private readonly CarManagementController _controller;

    public CarsControllerTests()
    {
        _mockService = new Mock<ICarService>();
        _controller = new CarManagementController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllCars_ReturnsOkResult_WithCars()
    {
        // Arrange
        var cars = GetTestCars();
        _mockService.Setup(service => service.GetAllCarsAsync()).ReturnsAsync(cars);

        // Act
        var result = await _controller.GetAllCars();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Car>>(actionResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetCar_ReturnsOkResult_WithCar()
    {
        // Arrange
        var carId = 1;
        var car = GetTestCars().First(c => c.Id == carId);
        _mockService.Setup(service => service.GetCarAsync(carId)).ReturnsAsync(car);

        // Act
        var result = await _controller.GetCar(carId);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Car>(actionResult.Value);
        Assert.Equal(carId, returnValue.Id);
    }

    [Fact]
    public async Task PostCar_ReturnsCreatedAtAction_WithCar()
    {
        // Arrange
        var newCar = new Car { Id = 3, Brand = "Honda", Model = "Civic", Year = 2022 };
        _mockService.Setup(service => service.CreateCarAsync(newCar)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PostCar(newCar);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Car>(actionResult.Value);
        Assert.Equal(newCar.Id, returnValue.Id);
    }

    [Fact]
    public async Task PostCar_ReturnsBadRequest_WhenModelIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Test", "TestError");

        var newCar = new Car();

        // Act
        var result = await _controller.PostCar(newCar);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task PutCar_ReturnsOkResult_WhenCarIsUpdated()
    {
        // Arrange
        var carId = 1;
        var carCreateDto = new CarCreateDto
        {
            Brand = "UpdatedBrand",
            Model = "UpdatedModel",
            Year = 2022,
            RegistryPlate = "UpdatedPlate",
            VinNumber = "UpdatedVin",
            IsAvailable = true
        };

        _mockService.Setup(service => service.UpdateCarAsync(carId, carCreateDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PutCar(carId, carCreateDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task PutCar_ReturnsBadRequest_WhenModelIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Test", "TestError");

        var carId = 1;
        var carCreateDto = new CarCreateDto();

        // Act
        var result = await _controller.PutCar(carId, carCreateDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCar_ReturnsNoContent_WhenCarExists()
    {
        // Arrange
        var carId = 1;
        _mockService.Setup(service => service.DeleteCarAsync(carId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteCar(carId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    private List<Car> GetTestCars()
    {
        return new List<Car>
        {
            new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
            new Car { Id = 2, Brand = "Ford", Model = "Fiesta", Year = 2018 }
        };
    }
}
