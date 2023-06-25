using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
