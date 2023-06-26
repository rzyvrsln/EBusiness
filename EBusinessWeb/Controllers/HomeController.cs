using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await employeeService.GetAllEmployeeAsync();
            return View(employees);
        }
    }
}
