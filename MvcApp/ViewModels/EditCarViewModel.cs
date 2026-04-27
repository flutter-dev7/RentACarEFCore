using System;
using System.ComponentModel.DataAnnotations;

namespace MvcApp.ViewModels;

public class EditCarViewModel
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Model { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greter than 0")]
    public decimal PricePerDay { get; set; }

    public bool IsAvialable { get; set; } = true;
}