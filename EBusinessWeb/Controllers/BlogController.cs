using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() => View();
    }
}
