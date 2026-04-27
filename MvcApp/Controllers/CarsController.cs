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
            var viewModel = await carService.CreateCarAsync(dto);
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
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
