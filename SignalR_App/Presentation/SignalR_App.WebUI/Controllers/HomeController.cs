using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.Dtos;
using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.WebUI.Models;

namespace SignalR_App.WebUI.Controllers
{
    public class HomeController(ILogger<HomeController> logger,
        IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IActionResult> Index()
        {
            var result = await _httpClientFactory.CreateClient("Sliders").GetAsync("Sliders");
            var products = await _httpClientFactory.CreateClient("ProductsWeb").GetAsync("Products/GetAllProductsForWeb");
            var sliders = await result.Content.ReadFromJsonAsync<List<SliderDto>>();
            var productResult = await products.Content.ReadFromJsonAsync<List<ProductDto>>();

            return View(new HomeViewModel()
            {
                Sliders = sliders ?? new(),
                Products = productResult ?? new()
            });
        }

    }
}
