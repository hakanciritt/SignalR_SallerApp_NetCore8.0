using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.TestimonialDtos;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController(ITestimonailService testimonialService) : ApiControllerBase
    {
        private readonly ITestimonailService _testimonialService = testimonialService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _testimonialService.GetAll();
            return Ok(result);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _testimonialService.GetById(id);
            return ActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestimonialDto testimonial)
        {
            var result = await _testimonialService.Create(testimonial);
            return ActionResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testimonialService.Delete(id);
            return ActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TestimonialDto testimonial)
        {
            var result = await _testimonialService.Update(testimonial);
            return ActionResult(result);
        }
    }
}
