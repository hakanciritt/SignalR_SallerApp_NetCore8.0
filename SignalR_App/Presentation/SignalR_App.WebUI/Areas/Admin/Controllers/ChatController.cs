using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Redis;
using SignalR_App.WebUI.Areas.Admin.Models;
using StackExchange.Redis;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    public class ChatController : AdminBaseController
    {
        private readonly RedisConfiguration _redis;

        public ChatController(RedisConfiguration redis)
        {
            _redis = redis;
        }

        public async Task<IActionResult> Index()
        {
            var servers = _redis.ConnectionMultiplexer.GetEndPoints();
            var connection = _redis.Connect();
            var keys = new List<RedisKey>();

            foreach (var item in servers)
            {
                var server = _redis.ConnectionMultiplexer.GetServer(item);
                keys.AddRange(server.Keys(pattern: "chatuser:*"));
            }

            return View(keys.Select(c => Convert.ToString(c)).ToList());
        }
        public async Task<PartialViewResult> GetMessageListByUser(string user)
        {
            var connection = _redis.Connect();

            var messages = await connection.StreamReadAsync(user.Replace("user-connection", "chatuser"), "0");
            var result = messages.Select(c => new ChatModel
            {
                UserName = c.Values?.FirstOrDefault(d => d.Name == "User").Value,
                Message = c.Values?.FirstOrDefault(d => d.Name == "Message").Value
            }).ToList();
            return PartialView(result);
        }
    }
}
