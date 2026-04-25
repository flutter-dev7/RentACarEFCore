using Domain.DTOs.Cars;
using Domain.Entities;
using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly ICarService _carService;

        public EditModel(ICarService carService)
        {
            _carService = carService;
        }

        [BindProperty]
        public UpdateCarDto Car { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);

            if (car == null)
                return NotFound();

            Car = new UpdateCarDto
            {
                Model = car.Model, 
                PricePerDay = car.PricePerDay,
                IsAvialable = car.IsAvialable
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _carService.UpdateCarAsync(id, Car);

            if(!result)
            {
                ModelState.AddModelError("", "Error updating car");
                return Page();
            }

            return RedirectToPage("/Cars/Cars");
        }
    }
}
