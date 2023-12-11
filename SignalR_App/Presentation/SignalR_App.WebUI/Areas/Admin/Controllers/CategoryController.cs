using Microsoft.AspNetCore.Mvc;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;
using SignalR_App.WebUI.Areas.Admin.Models.Categories;
using System.Text;
using System.Text.Json;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Categories");

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>("Categories");
            result ??= new();
            return View(result);
        }

        public async Task<IActionResult> CreateOrUpdate(int? categoryId)
        {
            if (!categoryId.HasValue) return View(new CategoryViewModel());
            var result = await _httpClient.GetFromJsonAsync<DataResult<CategoryViewModel>>($"Categories/{categoryId.Value}");
            if (result.Success) return View(result.Data);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CategoryViewModel model)
        {
            var requestModel = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            HttpResponseMessage message;

            if (model.Id == 0) message = await _httpClient.PostAsync("Categories/Create", requestModel, HttpContext.RequestAborted);
            else message = await _httpClient.PostAsync("Categories/Update", requestModel, HttpContext.RequestAborted);

            if (!message.IsSuccessStatusCode) return View();

            var content = JsonSerializer.Deserialize<Result>(await message.Content.ReadAsStringAsync());
            if (!content.Success)
            {
                TempData["ErrorMessage"] = content.Message;
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
