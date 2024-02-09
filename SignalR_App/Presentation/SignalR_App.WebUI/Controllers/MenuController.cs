using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.WebUI.Models;

namespace SignalR_App.WebUI.Controllers
{
    public class MenuController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IActionResult> Index()
        {
            var model = new MenuViewModel();
            var products = await _httpClientFactory.CreateClient("Products").GetAsync("Products/GetAllProductsForWeb");
            var productResult = await products.Content.ReadFromJsonAsync<List<ProductDto>>();
            model.Products = productResult ?? new();

            return View(model);
        }
    }
}
