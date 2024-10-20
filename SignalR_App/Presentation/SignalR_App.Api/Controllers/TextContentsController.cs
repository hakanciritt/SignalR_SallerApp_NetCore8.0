using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Dtos.TextContentDtos;
using SignalR_App.Application.Filters;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [CustomAuthorize]
    public class TextContentsController : ApiControllerBase
    {
        private readonly ITextContentService _textContentService;

        public TextContentsController(ITextContentService textContentService)
        {
            _textContentService = textContentService;
        }

        [HttpGet("about")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAbout()
        {
            var result = await _textContentService.GetTextContentByKey("About");
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _textContentService.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        [CustomAuthorize(Permissions.Pages)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _textContentService.GetAll();
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [CustomAuthorize(Permissions.PagesDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _textContentService.Delete(id);
            return Ok(result);
        }

        [HttpPut]
        [CustomAuthorize(Permissions.PagesCreateOrUpdate)]
        public async Task<IActionResult> Update(TextContentDto textContent)
        {
            var result = await _textContentService.Update(textContent);
            return ActionResult(result);
        }

        [HttpPost]
        [CustomAuthorize(Permissions.PagesCreateOrUpdate)]
        public async Task<IActionResult> Create(TextContentDto textContent)
        {
            var result = await _textContentService.Create(textContent);
            return ActionResult(result);
        }
    }
}
