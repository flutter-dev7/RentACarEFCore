using Domain.DTOs.Cars;
using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Cars
{
    public class CarsModel : PageModel
    {
        private readonly ICarService _carService;
        public CarsModel(ICarService carService)
        {
            _carService = carService;
        }
        public IEnumerable<GetCarDto> Cars { get; set; } = [];
        public async Task OnGet()
        {
            Cars = await _carService.GetAllCarsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _carService.DeleteCarAsync(id);

            if (!result)
            {
                ModelState.AddModelError("", "Error deleting car");
                return Page();
            }

            return RedirectToPage();
        }
    }
}
