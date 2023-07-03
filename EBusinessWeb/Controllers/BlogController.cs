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
        private readonly IEmployeeService employeeService;

        public BlogController(IPostService postService, IEmployeeService employeeService, IBlogService blogService)
        {
            this.postService = postService;
            this.employeeService = employeeService;
            this.blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            BlogAndPostVM vM = new BlogAndPostVM
            {
                Blogs = await blogService.GetAllBlogsAsync(),
                Posts = await postService.GetAllPostAsync(),
                Employees = await employeeService.GetAllEmployeeAsync()

            };

            return View(vM);
        }
        [HttpGet]
        public async Task<IActionResult> PostDetail(int id)
        {
            var post = await postService.GetPostByIdAsync(id);

            BlogAndPostVM vM = new BlogAndPostVM
            {
                Post = post,
                Posts = await postService.GetAllPostAsync(),
                Blogs = await blogService.GetAllBlogsAsync()

            };
            return View(vM);
        }
    }
}
