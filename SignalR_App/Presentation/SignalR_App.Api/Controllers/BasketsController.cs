using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.BasketDtos;
using SignalR_App.Application.WebServices;

namespace SignalR_App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController(IBasketWebService basketWebService) : ApiControllerBase
    {
        private readonly IBasketWebService _basketWebService = basketWebService;

        [HttpGet("{basketId:int}")]
        public async Task<IActionResult> GetBasketByTableNumber(int basketId)
        {
            var result = await _basketWebService.GetBasketByTableNumber(basketId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBasketDto basket)
        {

            return Ok();
        }
    }
}
