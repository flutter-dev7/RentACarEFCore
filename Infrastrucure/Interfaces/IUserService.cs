using Domain.DTOs.Users;

namespace Infrastrucure.Interfaces;

public interface IUserService
{
    Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
    Task<GetUserDto?> GetUserByIdAsync(int id);
    Task<bool> CreateUserAsync(CreateUserDto request);
    Task<bool> UpdateUserAsync(int id, UpdateUserDto request);
    Task<bool> DeleteUserAsync(int id);
}
