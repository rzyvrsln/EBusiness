using EBusinessEntity.Entities;
using EBusinessService.Extensions;
using EBusinessService.Services.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IValidator<Blog> validator;

        public BlogController(IBlogService blogService, IValidator<Blog> validator)
        {
            this.blogService = blogService;
            this.validator = validator;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await blogService.PaginationForBlogAsync(page));
        }
        [HttpGet]
        public async Task<IActionResult> Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Blog blog)
        {
            var result = await validator.ValidateAsync(blog);

            if (result.IsValid)
            {
                await blogService.AddBlogAsync(blog);
                return RedirectToAction("Add", "Blog");
            }

            else
                result.AddToModelStatte(this.ModelState);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await blogService.RemoveBlogAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await blogService.EditBlogAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            var result = await validator.ValidateAsync(blog);

            if (result.IsValid)
            {
                await blogService.EditPostBlogAsync(id, blog);
                return RedirectToAction($"{nameof(Index)}");
            }
            else
                result.AddToModelStatte(this.ModelState);

            return View();

        }
    }
}
