using Domain.DTOs.Cars;
using Domain.Entities;
using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApp.ViewModels;

namespace MvcApp.Controllers
{
    public class CarsController(ICarService carService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await carService.GetAllCarsAsync();
            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCarDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return View(dto);
            }

            string? imagePath = null;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            await carService.CreateCarAsync(dto, imagePath);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await carService.GetCarByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(new EditCarViewModel
            {
                Id = car.Id,
                Model = car.Model,
                PricePerDay = car.PricePerDay,
                IsAvialable = car.IsAvialable
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarViewModel viewModel)
        {
            await carService.UpdateCarAsync(viewModel.Id, new UpdateCarDto
            {
                Model = viewModel.Model,
                PricePerDay = viewModel.PricePerDay,
                IsAvialable = viewModel.IsAvialable
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await carService.DeleteCarAsync(id);
            return RedirectToAction("Index");
        }
    }
}
