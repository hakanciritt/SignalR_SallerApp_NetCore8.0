using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _textContentService.Delete(id);
            return Ok(result);
        }
    }
}
