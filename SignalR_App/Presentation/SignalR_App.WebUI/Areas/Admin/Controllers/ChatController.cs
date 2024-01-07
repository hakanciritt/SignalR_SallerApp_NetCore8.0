using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Redis;
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
    }
}
