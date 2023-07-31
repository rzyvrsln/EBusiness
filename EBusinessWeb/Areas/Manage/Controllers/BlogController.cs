using EBusinessEntity.Entities;
using EBusinessService.Extensions;
using EBusinessService.Services.Abstraction;
using EBusinessWeb.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IValidator<Blog> validator;
        private readonly IToastNotification toastNotification;

        public BlogController(IBlogService blogService, IValidator<Blog> validator, IToastNotification toastNotification)
        {
            this.blogService = blogService;
            this.validator = validator;
            this.toastNotification = toastNotification;
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
                toastNotification.AddSuccessToastMessage(Messages.Blog.Add(blog.Name));
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
