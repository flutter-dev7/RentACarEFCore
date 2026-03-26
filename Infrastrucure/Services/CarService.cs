using Domain.DTOs.Cars;
using Domain.Entities;
using Infrastrucure.Data;
using Infrastrucure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucure.Services;

public class CarService : ICarService
{
    private readonly AppDbContext _context;

    public CarService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetCarDto>> GetAllCarsAsync()
    {
        var cars = await _context.Cars
        .Select(c => new GetCarDto
        {
            Id = c.Id,
            Model = c.Model,
            PricePerDay = c.PricePerDay,
            IsAvialable = c.IsAvialable,
            BookingCount = c.Bookings.Count
        }).ToListAsync();

        return cars;
    }

    public async Task<GetCarDto?> GetCarByIdAsync(int id)
    {
        var car = await _context.Cars
        .Include(c => c.Bookings)
        .FirstOrDefaultAsync(c => c.Id == id);

        if (car == null)
        {
            return null;
        }

        return new GetCarDto
        {
            Id = car.Id,
            Model = car.Model,
            PricePerDay = car.PricePerDay,
            IsAvialable = car.IsAvialable,
            BookingCount = car.Bookings.Count
        };
    }

    public async Task<bool> CreateCarAsync(CreateCarDto request)
    {
        try
        {
            var car = new Car
            {
                Model = request.Model,
                PricePerDay = request.PricePerDay,
                IsAvialable = request.IsAvialable
            };

            _context.Cars.Add(car);

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateCarAsync(int id, UpdateCarDto request)
    {
        try
        {
            var car = await _context.Cars.FindAsync(id);

            if(car == null)
            {
                return false;
            }

            car.Model = request.Model;
            car.PricePerDay = request.PricePerDay;
            car.IsAvialable = request.IsAvialable;

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        try
        {
            var car = await _context.Cars.FindAsync(id);

            if(car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
