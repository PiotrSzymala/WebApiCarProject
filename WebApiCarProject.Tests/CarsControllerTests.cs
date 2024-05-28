using Microsoft.AspNetCore.Mvc;
using WebApiCarProject.Controllers;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Tests.MockingClass;

namespace WebApiCarProject.Tests;

public class CarsControllerTests
{
    private readonly CarManagementController _controller;
    private readonly CarRepositoryMock _mockRepo;

    public CarsControllerTests()
    {
        _mockRepo = new CarRepositoryMock();
        _controller = new CarManagementController(_mockRepo);
    }

    [Fact]
    public async Task GetAllCars_ReturnsOkResult_WithCars()
    {
        // Arrange & Act
        var result = await _controller.GetAllCars();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Car>>(actionResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetCar_ReturnsOkResult_WithCar()
    {
        // Arrange & Act
        var result = await _controller.GetCar(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Car>(actionResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    [Fact]
    public async Task GetCar_ReturnsNotFound_WhenCarDoesNotExist()
    {
        // Arrange & act
        var result = await _controller.GetCar(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
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

        // Act
        var result = await _controller.DeleteCar(existingCar.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteCar_ReturnsNotFound_WhenCarDoesNotExist()
    {
        // Arrange & Act
        var result = await _controller.DeleteCar(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}