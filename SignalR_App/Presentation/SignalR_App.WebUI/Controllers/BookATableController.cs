using Microsoft.AspNetCore.Mvc;
using SignalR_App.Domain.Result;
using SignalR_App.WebUI.Models;

namespace SignalR_App.WebUI.Controllers
{
    public class BookATableController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public IActionResult Index()
        {
            return View(new CreateBookingViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking(CreateBookingViewModel model)
        {
            var httpClient = _httpClientFactory.CreateClient("Bookings");
            var response = await httpClient.PostAsJsonAsync("Bookings", model);

            if (!response.IsSuccessStatusCode) return View();

            var content = await response.Content.ReadFromJsonAsync<Result>();
            if (!content.Success)
            {
                TempData["ErrorMessage"] = content.Message;
                return View(model);
            }

            return View();
        }
    }
}
