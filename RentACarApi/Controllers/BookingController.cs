using Domain.DTOs.Bookings;
using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RentACarApi.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingController(IBookingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var bookings = await _service.GetAllBookingsAsync();

        return Ok(bookings);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var booking = await _service.GetBookingByIdAsync(id);

        if(booking == null)
            return NotFound();

        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBookingDto request)
    {
        var res = await _service.CreateBookingAsync(request);

        return Created("Booking created successfully", res);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateBookingDto request)
    {
        var res = await _service.UpdateBookingAsync(id, request);

        if(!res)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var res = await _service.DeleteBookingAsync(id);

        if(!res) 
            return NotFound();

        return NoContent();
    }
}
