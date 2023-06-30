using EBusinessData.DAL;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EBusinessWeb.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService postService;
        private readonly IBlogService blogService;
        private readonly AppDbContext dbContext;

        public BlogController(IPostService postService, AppDbContext dbContext)
        {
            this.postService = postService;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            BlogAndPostVM vM = new BlogAndPostVM
            {
                Blogs =  dbContext.Blogs,
                Posts = dbContext.Posts
            };

            return View(vM);
        }
        [HttpGet]
        public async Task<IActionResult> PostDetail() => View();
    }
}
