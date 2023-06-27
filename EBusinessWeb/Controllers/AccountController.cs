using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index() => View();
    }
}
