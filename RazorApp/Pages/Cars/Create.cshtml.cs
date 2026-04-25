using Domain.DTOs.Cars;
using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Cars;

public class CreateModel : PageModel
{
    private readonly ICarService _carService;

    public CreateModel(ICarService carService)
    {
        _carService = carService;
    }

    [BindProperty]
    public CreateCarDto Car { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _carService.CreateCarAsync(Car);

        if (!result)
        {
            ModelState.AddModelError("", "Error while creating car");
            return Page();
        }

        return RedirectToPage("/Cars/Cars");
    }
}