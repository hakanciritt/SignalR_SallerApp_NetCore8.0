using Microsoft.AspNetCore.Mvc;
using SignalR_App.WebUI.Models;

namespace SignalR_App.WebUI.Controllers
{
    public class BasketController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IActionResult> Index()
        {
            //var result = await _httpClientFactory.CreateClient("Baskets").GetAsync("Baskets");
            //var sliders = await result.Content.ReadFromJsonAsync<List<BasketDto>>();

            var model = new BasketViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket()
        {
            var client = _httpClientFactory.CreateClient("Baskets");
            return View();
        }
    }
}
