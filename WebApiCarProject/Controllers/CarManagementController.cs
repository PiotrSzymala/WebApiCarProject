using Microsoft.AspNetCore.Mvc;
using WebApiCarProject.Application.Services;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Infrastructure.Repositories;
using WebApiCarProject.Models.Dtos;

namespace WebApiCarProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarManagementController : ControllerBase
{
    private readonly ICarService _carService;

    public CarManagementController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await _carService.GetAllCarsAsync();

        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCar(int id)
    {
        var car = await _carService.GetCarAsync(id);

        return Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> PostCar([FromBody] Car car)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _carService.CreateCarAsync(car);

        return CreatedAtAction("GetCar", new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCar(int id, [FromBody] CarCreateDto carCreateDto)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _carService.UpdateCarAsync(id, carCreateDto);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        await _carService.DeleteCarAsync(id);

        return Ok();
    }
}