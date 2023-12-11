using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.CategoryDto;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ApiControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryService.GetById(id);
            return ActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            var result = await _categoryService.Create(category);
            return ActionResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            return ActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto category)
        {
            var result = await _categoryService.Update(category);
            return ActionResult(result);
        }
    }
}
