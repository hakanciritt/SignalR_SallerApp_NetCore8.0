using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Dtos.ContactDtos;
using SignalR_App.Application.Filters;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [CustomAuthorize(Permissions.Contacts)]
    public class ContactsController(IContactService contactService) : ApiControllerBase
    {
        private readonly IContactService _contactService = contactService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _contactService.GetById(id);
            return ActionResult(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ContactDto contact)
        {
            var result = await _contactService.Create(contact);
            return ActionResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactService.Delete(id);
            return ActionResult(result);
        }
    }
}
