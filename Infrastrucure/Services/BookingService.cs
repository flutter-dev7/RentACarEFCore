using Domain.DTOs.Bookings;
using Domain.Entities;
using Infrastrucure.Data;
using Infrastrucure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucure.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetBookingDto>> GetAllBookingsAsync()
    {
        var bookings = await _context.Bookings
        .Select(b => new GetBookingDto
        {
            Id = b.Id,
            UserId = b.UserId,
            Username = b.User.Username,
            CarId = b.CarId,
            CarModel = b.Car.Model,
            StartDate = b.StartDate,
            EndDate = b.EndDate,
            TotalPrice = b.TotalPrice
        }).ToListAsync();

        return bookings;
    }

    public async Task<GetBookingDto?> GetBookingByIdAsync(int id)
    {
        var booking = await _context.Bookings
        .Include(b => b.Car)
        .Include(b => b.User)
        .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return null;
        }

        return new GetBookingDto
        {
            Id = booking.Id,
            UserId = booking.UserId,
            Username = booking.User.Username,
            CarId = booking.CarId,
            CarModel = booking.Car.Model,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            TotalPrice = booking.TotalPrice
        };
    }

    public async Task<bool> CreateBookingAsync(CreateBookingDto request)
    {

        if (request.StartDate > request.EndDate)
            throw new ArgumentException("StartDate cannot be greater than EndDate");


        var car = await _context.Cars.FindAsync(request.CarId);
        if (car == null)
            throw new Exception("Car not found");


        var days = (request.EndDate - request.StartDate).Days;
        if (days <= 0)
            throw new ArgumentException("Booking must be at least 1 day");


        var totalPrice = days * car.PricePerDay;

        var booking = new Booking
        {
            UserId = request.UserId,
            CarId = request.CarId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalPrice = totalPrice
        };

        _context.Bookings.Add(booking);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateBookingAsync(int id, UpdateBookingDto request)
    {
        try
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return false;

            if (request.StartDate > request.EndDate)
                throw new ArgumentException("StartDate cannot be greater than EndDate");


            var car = await _context.Cars.FindAsync(request.CarId);
            if (car == null)
                throw new Exception("Car not found");


            var days = (request.EndDate - request.StartDate).Days;
            if (days <= 0)
                throw new ArgumentException("Booking must be at least 1 day");


            var totalPrice = days * car.PricePerDay;

            booking.UserId = request.UserId;
            booking.CarId = request.CarId;
            booking.StartDate = request.StartDate;
            booking.EndDate = request.EndDate;

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        try
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
