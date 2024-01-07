using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.BookingDto;
using SignalR_App.Application.Filters;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [CustomAuthorize]
    public class BookingsController(IBookingService bookingService) : ApiControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookingService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _bookingService.GetById(id);
            return ActionResult(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(BookingDto booking)
        {
            var result = await _bookingService.Create(booking);
            return ActionResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookingService.Delete(id);
            return ActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookingDto booking)
        {
            var result = await _bookingService.Update(booking);
            return ActionResult(result);
        }
    }
}
