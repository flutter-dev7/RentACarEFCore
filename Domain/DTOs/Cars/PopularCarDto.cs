using System;

namespace Domain.DTOs.Cars;

public class PopularCarDto
{
    public string Model { get; set; } = string.Empty;
    public int BookingCount { get; set; }
}
