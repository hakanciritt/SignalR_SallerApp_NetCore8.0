using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Filters;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;
using SignalR_App.WebUI.Areas.Admin.Models.Categories;
using SignalR_App.WebUI.Areas.Admin.Models.Products;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    public class ProductController(IHttpClientFactory httpClientFactory) : AdminBaseController
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Products");
        private readonly HttpClient _httpClientForCategory = httpClientFactory.CreateClient("Categories");

        [CustomAuthorizeWeb(Permissions.ProductRead)]
        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductViewModel>>("Products");
            return View(result);
        }

        [CustomAuthorizeWeb(Permissions.ProductRead)]
        public async Task<IActionResult> CreateOrUpdate(int? productId)
        {
            var categories = await _httpClientForCategory.GetFromJsonAsync<List<CategoryViewModel>>("Categories", HttpContext.RequestAborted);
            ViewBag.Categories = categories;
            if (!productId.HasValue) return View(new ProductViewModel());
            var result = await _httpClient.GetFromJsonAsync<DataResult<ProductViewModel>>($"Products/{productId.Value}");

            if (result.Success) return View(result.Data);
            return RedirectToAction("Index");
        }

        [CustomAuthorizeWeb(Permissions.ProductCreateOrUpdate)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(ProductViewModel model)
        {
            model.Category = null;
            var requestModel = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            HttpResponseMessage message;

            if (model.Id == 0) message = await _httpClient.PostAsync("Products", requestModel);
            else message = await _httpClient.PutAsync("Products", requestModel);
            var response = await message.Content.ReadFromJsonAsync<Result>();
            return Json(response);
        }

        [CustomAuthorizeWeb(Permissions.ProductDelete)]
        public async Task<IActionResult> Delete(int productId)
        {
            var request = await _httpClient.DeleteAsync($"Products/{productId}", HttpContext.RequestAborted);
            if (!request.IsSuccessStatusCode) return Json(new Result());
            var response = await request.Content.ReadFromJsonAsync<Result>();
            return Json(response);
        }
    }
}
