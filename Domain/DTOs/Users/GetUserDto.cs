using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Users;

public class GetUserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public int BookingCount { get; set; }
}
