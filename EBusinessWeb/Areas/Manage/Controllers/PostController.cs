using EBusinessData.DAL;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly AppDbContext dbContext;

        public PostController(IPostService postService, AppDbContext dbContext)
        {
            this.postService = postService;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ICollection<Post> posts = await postService.GetAllPostAsync();
            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Blogs = new SelectList(dbContext.Blogs, nameof(Blog.Id), nameof(Blog.Name));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostVM postVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Blogs = new SelectList(dbContext.Blogs, nameof(Blog.Id), nameof(Blog.Name));
                return View();
            }
            await postService.AddPostAsync(postVM);
            return RedirectToAction("Add", "Post");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await postService.RemovePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
