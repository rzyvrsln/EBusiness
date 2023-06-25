using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class BlogDetailController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() => View();
    }
}
