using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await contactService.GetAllContactsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await contactService.RemoveContactAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public Task<IActionResult> Reply(int id)
        //{

        //}
    }
}
