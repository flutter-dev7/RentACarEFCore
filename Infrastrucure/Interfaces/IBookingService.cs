using Domain.DTOs.Bookings;
using Domain.Entities;

namespace Infrastrucure.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<GetBookingDto>> GetAllBookingsAsync();
    Task<GetBookingDto?> GetBookingByIdAsync(int id);
    Task<bool> CreateBookingAsync(CreateBookingDto request);
    Task<bool> UpdateBookingAsync(int id, UpdateBookingDto request);
    Task<bool> DeleteBookingAsync(int id);
}
