using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Dtos.CategoryDto;
using SignalR_App.Application.Filters;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [CustomAuthorize]
    public class CategoriesController(ICategoryService categoryService) : ApiControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet]
        [CustomAuthorize(Permissions.CategoryRead)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [CustomAuthorize(Permissions.CategoryRead)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryService.GetById(id);
            return ActionResult(result);
        }

        [HttpPost]
        [CustomAuthorize(Permissions.CategoryCreateOrUpdate)]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            var result = await _categoryService.Create(category);
            return ActionResult(result);
        }

        [HttpDelete("{id:int}")]
        [CustomAuthorize(Permissions.CategoryDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            return ActionResult(result);
        }

        [HttpPut]
        [CustomAuthorize(Permissions.CategoryCreateOrUpdate)]
        public async Task<IActionResult> Update(CategoryDto category)
        {
            var result = await _categoryService.Update(category);
            return ActionResult(result);
        }
    }
}
