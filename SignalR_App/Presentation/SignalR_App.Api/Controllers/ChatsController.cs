using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application;
using SignalR_App.Application.Dtos;
using SignalR_App.Application.Filters;
using SignalR_App.Application.Redis;
using StackExchange.Redis;

namespace SignalR_App.Api.Controllers
{
    [CustomAuthorize(Permissions.Messages)]
    public class ChatsController(RedisConfiguration redis) : ApiControllerBase
    {
        private readonly RedisConfiguration _redis = redis;

        [HttpGet]
        public async Task<IActionResult> GetAllUserList()
        {
            var servers = _redis.ConnectionMultiplexer.GetEndPoints();
            var connection = _redis.Connect();
            var keys = new List<RedisKey>();

            foreach (var item in servers)
            {
                var server = _redis.ConnectionMultiplexer.GetServer(item);
                keys.AddRange(server.Keys(pattern: "user-connection:*"));
            }
            List<KeyValuePair<string, string>> dictionary = new();

            foreach (var item in keys)
            {
                var value = await connection.HashGetAsync(item, "TempUserName");
                dictionary.Add(new KeyValuePair<string, string>(item.ToString(), value.ToString()));
            }

            return Ok(dictionary);
        }

        [HttpGet("{user}")]
        public async Task<IActionResult> GetMessageListByUser([FromRoute] string user)
        {
            var connection = _redis.Connect();

            var messages = await connection.StreamReadAsync(user.Replace("user-connection", "chatuser"), "0");

            var result = messages.Select(c => new ChatDto
            {
                UserName = c.Values?.FirstOrDefault(d => d.Name == "User").Value!,
                Message = c.Values?.FirstOrDefault(d => d.Name == "Message").Value!
            }).ToList();

            return Ok(result);
        }
    }
}
