using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Filters;
using SignalR_App.Domain.Result;
using SignalR_App.WebUI.Areas.Admin.Models.TextContents;
using System.Text;
using System.Text.Json;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    public class TextContentController(IHttpClientFactory httpClientFactory) : AdminBaseController
    {

        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("TextContent");

        [CustomAuthorizeWeb(Permissions.Pages)]
        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TextContentViewModel>>("TextContents");
            return View(result);
        }
        [CustomAuthorizeWeb(Permissions.PagesCreateOrUpdate)]
        public async Task<IActionResult> CreateOrUpdate(int? textContentId)
        {
            if (!textContentId.HasValue) return View(new TextContentViewModel());
            var result = await _httpClient.GetFromJsonAsync<TextContentViewModel>($"TextContents/{textContentId.Value}");

            if (result is not null) return View(result);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(TextContentViewModel model)
        {
            var requestModel = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            HttpResponseMessage message;

            if (model.Id == 0) message = await _httpClient.PostAsync("TextContents", requestModel);
            else message = await _httpClient.PutAsync("TextContents", requestModel);
            var response = await message.Content.ReadFromJsonAsync<Result>();
            return Json(response);
        }

        public async Task<IActionResult> Delete(int textContentId)
        {
            var request = await _httpClient.DeleteAsync($"TextContents/{textContentId}", HttpContext.RequestAborted);
            if (!request.IsSuccessStatusCode) return Json(new Result());
            var response = await request.Content.ReadFromJsonAsync<Result>();
            return Json(response);
        }
    }
}

