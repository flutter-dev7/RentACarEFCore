using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Bookings;

public class CreateBookingDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int CarId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }    
}
