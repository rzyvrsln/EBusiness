using EBusinessData.DAL;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly AppDbContext context;

        public EmployeeController(AppDbContext context, IEmployeeService service)
        {
            this.context = context;
            this.employeeService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ICollection<Employee> employees = await employeeService.GetAllEmployeeAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Positions = new SelectList(context.Positions, nameof(Position.Id), nameof(Position.Name));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            await employeeService.AddEmployeeAsync(employeeVM);
            return View();
        }

    }
}
