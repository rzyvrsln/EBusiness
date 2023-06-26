using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View();

        [HttpPost]
        public async Task<IActionResult> Send(Contact contact)
        {
            await contactService.AddContactAsync(contact);
            return RedirectToAction("Index");
        }
    }
}
