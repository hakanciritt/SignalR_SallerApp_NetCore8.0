using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.BookingDto;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(IBookingService bookingService) : ControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookingService.GetAll();
            return Ok(result);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _bookingService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingDto booking)
        {
            var result = await _bookingService.Create(booking);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookingService.Delete(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookingDto booking)
        {
            var result = await _bookingService.Update(booking);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
