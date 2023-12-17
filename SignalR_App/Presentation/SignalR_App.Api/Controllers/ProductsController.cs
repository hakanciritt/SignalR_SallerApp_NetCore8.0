using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ApiControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetById(id);
            return ActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            var result = await _productService.Create(product);
            return ActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _productService.Delete(id);
            return ActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto product)
        {
            var result = await _productService.Update(product);
            return ActionResult(result);
        }
    }
}
