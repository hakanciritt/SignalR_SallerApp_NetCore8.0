using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.TextContentDtos;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextContentsController : ControllerBase
    {
        private readonly ITextContentService _textContentService;

        public TextContentsController(ITextContentService textContentService)
        {
            _textContentService = textContentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAbout()
        {
            var result = await _textContentService.GetTextContentByKey("About");
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _textContentService.Delete(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TextContentDto textContent)
        {
            var result = await _textContentService.Update(textContent);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TextContentDto textContent)
        {
            var result = await _textContentService.Create(textContent);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
