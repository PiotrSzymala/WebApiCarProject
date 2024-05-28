using Microsoft.AspNetCore.Mvc;
using WebApiCarProject.Infrastructure.Entities;
using WebApiCarProject.Infrastructure.Repositories;

namespace WebApiCarProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarManagementController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    public CarManagementController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await _carRepository.GetAllCarsAsync();
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCar(long id)
    {
        var car = await _carRepository.GetCarAsync(id);

        if (car == null)
            return NotFound();

        return Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> PostCar([FromBody] Car car)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _carRepository.InsertCarAsync(car);
        await _carRepository.SaveAsync();

        return CreatedAtAction("GetCar", new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCar(long id, [FromBody] Car car)
    {
        if (id != car.Id)
            return BadRequest();

        //todo update logic.

        await _carRepository.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(long id)
    {
        var car = await _carRepository.GetCarAsync(id);
        if (car == null)
            return NotFound();

        await _carRepository.DeleteCarAsync(id);
        await _carRepository.SaveAsync();

        return NoContent();
    }
}