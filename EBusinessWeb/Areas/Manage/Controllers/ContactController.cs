using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public async Task<IActionResult> Reply(int id)
        {
            return View(await contactService.GetContactByIdAsync(id));
        }
    }
}
