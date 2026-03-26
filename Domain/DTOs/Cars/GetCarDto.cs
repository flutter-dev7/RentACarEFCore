namespace Domain.DTOs.Cars;

public class GetCarDto
{
    public int Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public bool IsAvialable { get; set; } = true;
    public int BookingCount { get; set; }
}
