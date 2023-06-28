using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await positionService.GetAllPositionsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Position position)
        {
            if (!ModelState.IsValid) return View();
            await positionService.AddPositionAsync(position);
            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            if (!ModelState.IsValid) return View();
            await positionService.RemovePositionAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid) return View();
            return View(await positionService.EditPositionAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Position position)
        {
            if (!ModelState.IsValid) return View();
            await positionService.EditPositionPostAsync(id, position);
            return RedirectToAction(nameof(Index));

        }
    }
}
