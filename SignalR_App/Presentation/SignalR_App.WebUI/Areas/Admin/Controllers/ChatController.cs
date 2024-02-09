using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    public class ChatController : AdminBaseController
    {
        private readonly HttpClient _httpClient;

        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Chats");
        }

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetFromJsonAsync<List<KeyValuePair<string, string>>>("Chats");
            return View(result);
        }
        public async Task<PartialViewResult> GetMessageListByUser(string user)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ChatDto>>($"Chats/{user}");

            return PartialView(result);
        }
    }
}
