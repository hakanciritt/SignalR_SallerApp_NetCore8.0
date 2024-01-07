using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Filters;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Application.WebServices;

namespace SignalR_App.Api.Controllers
{

    public class SlidersController(ISliderService sliderService, IWebService webService) : ApiControllerBase
    {
        private readonly ISliderService _sliderService = sliderService;
        private readonly IWebService _webService = webService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _webService.GetAllSliders();
            return Ok(result);
        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> Get([FromRoute] int id)
        //{
        //    var result = await _productService.GetById(id);
        //    return ActionResult(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(ProductDto product)
        //{
        //    var result = await _productService.Create(product);
        //    return ActionResult(result);
        //}

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    var result = await _productService.Delete(id);
        //    return ActionResult(result);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(ProductDto product)
        //{
        //    var result = await _productService.Update(product);
        //    return ActionResult(result);
        //}
    }
}
