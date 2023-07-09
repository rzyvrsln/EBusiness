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
        private readonly ICommentService commentService;

        public BlogController(IPostService postService, IEmployeeService employeeService, IBlogService blogService, ICommentService commentService)
        {
            this.postService = postService;
            this.employeeService = employeeService;
            this.blogService = blogService;
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {

            BlogAndPostVM vM = new BlogAndPostVM
            {
                Blogs = await blogService.GetAllBlogsAsync(),
                Posts = await postService.GetAllPostAsync(),
                Employees = await employeeService.GetAllEmployeeAsync(),
                PaginationVM = await postService.PaginationForWebPagePostAsync(page)
            };

            return View(vM);
        }
        [HttpGet]
        public async Task<IActionResult> PostDetail(int id)
        {
            var post = await postService.GetPostByIdAsync(id);
            if(post is not null)
            {
                BlogAndPostVM vM = new BlogAndPostVM
                {
                    Post = post,
                    Posts = await postService.GetAllPostAsync(),
                    Blogs = await blogService.GetAllBlogsAsync(),
                    Comments = await commentService.GetAllIncludeCommentsAsync()
                };

                return View(vM);
            }
            return RedirectToAction(nameof(Index),"Home");
        }

        [HttpPost]
        public async Task<IActionResult> Comment(Comment comment)
        {
            if (!ModelState.IsValid) return View();
            await commentService.AddCommentAsync(comment);
            return RedirectToAction(nameof(PostDetail));
        }
    }
}
