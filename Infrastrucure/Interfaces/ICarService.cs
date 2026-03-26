using Domain.DTOs.Cars;

namespace Infrastrucure.Interfaces;

public interface ICarService
{
    Task<IEnumerable<GetCarDto>> GetAllCarsAsync();
    Task<GetCarDto?> GetCarByIdAsync(int id);
    Task<bool> CreateCarAsync(CreateCarDto request);
    Task<bool> UpdateCarAsync(int id, UpdateCarDto request);
    Task<bool> DeleteCarAsync(int id);
}
