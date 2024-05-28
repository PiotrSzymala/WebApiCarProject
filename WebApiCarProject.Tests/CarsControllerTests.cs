using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiCarProject.Controllers;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Infrastructure.Repositories;

namespace WebApiCarProject.Tests;

public class CarsControllerTests
{
    private readonly CarManagementController _controller;
    private readonly Mock<ICarRepository> _mockRepo;

    public CarsControllerTests()
    {
        _mockRepo = new Mock<ICarRepository>();
        _controller = new CarManagementController(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllCars_ReturnsOkResult_WithCars()
    {
        // Arrange
        var mockCars = new List<Car>
        {
            new() { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
            new() { Id = 2, Brand = "Honda", Model = "Civic", Year = 2019 }
        };
        _mockRepo.Setup(repo => repo.GetAllCarsAsync()).ReturnsAsync(mockCars);

        // Act
        var result = await _controller.GetAllCars();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Car>>(actionResult.Value);
        Assert.Equal(2, returnValue.Count);
        _mockRepo.Verify(repo => repo.GetAllCarsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCar_ReturnsOkResult_WithCar()
    {
        // Arrange
        var mockCar = new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 };
        _mockRepo.Setup(repo => repo.GetCarAsync(1)).ReturnsAsync(mockCar);

        // Act
        var result = await _controller.GetCar(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Car>(actionResult.Value);
        Assert.Equal(mockCar.Id, returnValue.Id);
        _mockRepo.Verify(repo => repo.GetCarAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetCar_ReturnsNotFound_WhenCarDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetCarAsync(It.IsAny<int>())).ReturnsAsync(() => null);

        // Act
        var result = await _controller.GetCar(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockRepo.Verify(repo => repo.GetCarAsync(999), Times.Once);
    }

    [Fact]
    public async Task PostCar_ReturnsCreatedAtAction_WithCar()
    {
        // Arrange
        var newCar = new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 };

        // Act
        var result = await _controller.PostCar(newCar);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Car>(actionResult.Value);
        Assert.Equal(newCar.Id, returnValue.Id);
        _mockRepo.Verify(repo => repo.InsertCarAsync(newCar), Times.Once);
        _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
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
    public async Task DeleteCar_ReturnsNoContent_WhenCarExists()
    {
        // Arrange
        var existingCar = new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 };
        _mockRepo.Setup(repo => repo.GetCarAsync(existingCar.Id)).ReturnsAsync(existingCar);

        // Act
        var result = await _controller.DeleteCar(existingCar.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.DeleteCarAsync(existingCar.Id), Times.Once);
        _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteCar_ReturnsNotFound_WhenCarDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetCarAsync(It.IsAny<int>())).ReturnsAsync(() => null);

        // Act
        var result = await _controller.DeleteCar(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockRepo.Verify(repo => repo.GetCarAsync(999), Times.Once);
        _mockRepo.Verify(repo => repo.DeleteCarAsync(It.IsAny<int>()), Times.Never);
    }
}