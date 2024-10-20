using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Filters;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;
using SignalR_App.WebUI.Areas.Admin.Models.Categories;
using System.Text;
using System.Text.Json;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    public class CategoryController(IHttpClientFactory httpClientFactory) : AdminBaseController
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Categories");

        [CustomAuthorizeWeb(Permissions.CategoryRead)]
        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>("Categories");
            result ??= new();
            return View(result);
        }

        [CustomAuthorizeWeb(Permissions.CategoryRead)]
        public async Task<IActionResult> CreateOrUpdate(int? categoryId)
        {
            if (!categoryId.HasValue) return View(new CategoryViewModel());
            var result = await _httpClient.GetFromJsonAsync<DataResult<CategoryViewModel>>($"Categories/{categoryId.Value}");
            if (result.Success) return View(result.Data);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [CustomAuthorizeWeb(Permissions.CategoryCreateOrUpdate)]
        public async Task<IActionResult> CreateOrUpdate(CategoryViewModel model)
        {
            var requestModel = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            HttpResponseMessage message;

            if (model.Id == 0) message = await _httpClient.PostAsync("Categories", requestModel, HttpContext.RequestAborted);
            else message = await _httpClient.PutAsync("Categories", requestModel, HttpContext.RequestAborted);

            if (!message.IsSuccessStatusCode) return View(model);

            var content = JsonSerializer.Deserialize<Result>(await message.Content.ReadAsStringAsync());
            if (!content.Success)
            {
                TempData["ErrorMessage"] = content.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [CustomAuthorizeWeb(Permissions.CategoryDelete)]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var request = await _httpClient.DeleteAsync($"Categories/{categoryId}", HttpContext.RequestAborted);
            if (!request.IsSuccessStatusCode) return RedirectToAction("Index");

            var response = JsonSerializer.Deserialize<Result>(await request.Content.ReadAsStringAsync());
            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
    }
}
