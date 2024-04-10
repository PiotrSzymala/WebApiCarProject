using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiCarProject.Controllers;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Infrastructure.Repositories;

namespace WebApiCarProject.Tests;

public class CarManagementTests
{
    [Fact]
    public async Task PostCar_InvalidModelState_ReturnsBadRequest()
    {
        var mockRepository = new Mock<ICarRepository>();
        var controller = new CarManagementController(mockRepository.Object);
        controller.ModelState.AddModelError("key", "error message");
        var car = new Car { /* Initialize with invalid data if necessary */ };

        var result = await controller.PostCar(car);

        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(actionResult.Value);
    }

    [Fact]
    public async Task PutCar_IdMismatch_ReturnsBadRequest()
    {
        var mockRepository = new Mock<ICarRepository>();
        var controller = new CarManagementController(mockRepository.Object);
        var testCar = new Car { Id = 1, /* inne właściwości samochodu */ };
        var differentId = 2;

        var result = await controller.PutCar(differentId, testCar);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteCar_NonexistentId_ReturnsNotFound()
    {
        var mockRepository = new Mock<ICarRepository>();
        var nonExistentId = 999;
        mockRepository.Setup(repo => repo.GetCarAsync(nonExistentId))
            .ReturnsAsync((Car)null);

        var controller = new CarManagementController(mockRepository.Object);

        var result = await controller.DeleteCar(nonExistentId);

        Assert.IsType<NotFoundResult>(result);
    }
}