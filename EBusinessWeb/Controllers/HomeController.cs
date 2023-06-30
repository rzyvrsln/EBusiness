using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IPostService postService;

        public HomeController(IEmployeeService employeeService, IPostService postService)
        {
            this.employeeService = employeeService;
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            BlogAndPostVM vM = new BlogAndPostVM
            {
                Employees = await employeeService.GetAllEmployeeAsync(),
                Posts = await postService.GetAllPostAsync()
            };
            return View(vM);
        }
    }
}
