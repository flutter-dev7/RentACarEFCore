using Domain.DTOs.Cars;
using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RentACarApi.Controllers;

[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly ICarService _service;

    public CarController(ICarService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var cars = await _service.GetAllCarsAsync();

        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var car = await _service.GetCarByIdAsync(id);

        if(car == null)
        {
            return NotFound();
        }

        return Ok(car);
    } 

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCarDto request)
    {
        var res = await _service.CreateCarAsync(request);

        return Created("Car created successfully", res);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateCarDto request)
    {
        var res = await _service.UpdateCarAsync(id, request);

        if(!res)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var res = await _service.DeleteCarAsync(id);

        if(!res)
        {
            return NotFound();
        }

        return NoContent();
    }
}
