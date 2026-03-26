using Domain.DTOs.Users;
using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RentACarApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _service.GetAllUsersAsync();

        return Ok(users);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var user = await _service.GetUserByIdAsync(id);

        if(user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUserDto request)
    {
        var res = await _service.CreateUserAsync(request);

        return Created("User created successfully", res);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateUserDto request)
    {
        var res = await _service.UpdateUserAsync(id, request);

        if(!res)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var res = await _service.DeleteUserAsync(id);

        if(!res)
        {
            return NotFound();
        }

        return NoContent();
    }
}
