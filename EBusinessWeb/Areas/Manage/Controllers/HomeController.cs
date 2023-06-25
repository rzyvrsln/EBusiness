using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() => View();
    }
}
