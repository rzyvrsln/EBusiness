using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await blogService.GetAllBlogsAsync();
            return View(blogs);
        }
        [HttpGet]
        public async Task<IActionResult> Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Blog blog)
        {
            if(!ModelState.IsValid) { return View(); }
            await blogService.AddBlogAsync(blog);
            return RedirectToAction("Add", "Blog");
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
            if(!ModelState.IsValid) { return View(); }
            await blogService.EditPostBlogAsync(id, blog);
            return RedirectToAction($"{nameof(Index)}");
        }
    }
}
