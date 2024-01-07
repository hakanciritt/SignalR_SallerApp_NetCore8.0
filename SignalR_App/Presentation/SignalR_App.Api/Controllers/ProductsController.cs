using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.Application.Filters;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Application.WebServices;

namespace SignalR_App.Api.Controllers
{

    public class ProductsController(IProductService productService, IWebService webService) : ApiControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IWebService _webService = webService;

        [HttpGet]
        [CustomAuthorize(Permissions.ProductRead)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [CustomAuthorize(Permissions.ProductRead)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _productService.GetById(id);
            return ActionResult(result);
        }

        [HttpPost]
        [CustomAuthorize(Permissions.ProductCreateOrUpdate)]
        public async Task<IActionResult> Create(ProductDto product)
        {
            var result = await _productService.Create(product);
            return ActionResult(result);
        }

        [HttpDelete("{id:int}")]
        [CustomAuthorize(Permissions.ProductDelete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _productService.Delete(id);
            return ActionResult(result);
        }

        [HttpPut]
        [CustomAuthorize(Permissions.ProductCreateOrUpdate)]
        public async Task<IActionResult> Update(ProductDto product)
        {
            var result = await _productService.Update(product);
            return ActionResult(result);
        }

        [HttpGet("GetAllProductsForWeb")]
        public async Task<IActionResult> GetAllProductsForWeb()
        {
            var products = await _webService.GetAllProducts();
            return Ok(products);
        }
    }
}
