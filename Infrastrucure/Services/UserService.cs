using Domain.DTOs.Users;
using Domain.Entities;
using Infrastrucure.Data;
using Infrastrucure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users
        .Select(u => new GetUserDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Phone = u.Phone,
            BookingCount = u.Bookings.Count   
        }).ToListAsync();

        return users;
    }

    public async Task<GetUserDto?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users
        .Include(u => u.Bookings)
        .FirstOrDefaultAsync(u => u.Id == id);

        if(user == null)
        {
            return null;
        }

        return new GetUserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone,
            BookingCount = user.Bookings.Count
        };
    }
    public async Task<bool> CreateUserAsync(CreateUserDto request)
    {
        try
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Phone = request.Phone
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto request)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return false;
            }

            user.Username = request.Username;
            user.Email = request.Email;
            user.Phone = request.Phone;
            
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return false;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
