using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() => View();
    }
}
