using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Redis;
using SignalR_App.Domain.Entitites;
using StackExchange.Redis;

namespace SignalR_App.Application.Hubs
{
    public class MessageHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RedisConfiguration _redisConfiguration;

        public MessageHub(UserManager<AppUser> userManager,
            RedisConfiguration redisConfiguration)
        {
            _redisConfiguration = redisConfiguration;
            _userManager = userManager;
        }
        public async Task SendMessageToAdmin(string message)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == 1);
            var address = Context.GetHttpContext()?.Connection?.RemoteIpAddress?.ToString();
            var redis = _redisConfiguration.Connect();

            string key = $"chatuser:{address}";

            //await redis.HashSetAsync($"user-connection:{address}",
            //[
            //    new HashEntry("ConnectionId", Context.ConnectionId),
            //    new HashEntry("TempUserName", "Hakan Cirit")
            //]);

            await redis.HashSetAsync($"user-connection:{address}", "ConnectionId", Context.ConnectionId);

            var tempUserName = await redis.HashGetAsync($"user-connection:{address}", "TempUserName");
            tempUserName = !string.IsNullOrEmpty(tempUserName.ToString()) ? tempUserName : "Hakan Cirit";
            await redis.StreamAddAsync(key, new[] {
                new NameValueEntry("User", tempUserName),
                new NameValueEntry("Message", message)
            });

            //await Clients.User(user.Id.ToString()).SendAsync(ReceiverNames.ReceiveMessageForCustomer, message);
            await Clients.All.SendAsync(ReceiverNames.ReceiveMessageForCustomer, message);
        }

        public async Task SendMessageToCustomer(string user, string message)
        {
            var redis = _redisConfiguration.Connect();

            var connectionId = await redis.HashGetAsync(user, "ConnectionId");

            await redis.StreamAddAsync(user.Replace("user-connection", "chatuser"), new[] {
                new NameValueEntry("User", "Admin"),
                new NameValueEntry("Message", message)
            });

            await Clients.Client(connectionId.ToString()).SendAsync(ReceiverNames.ReceiveMessageForAdmin, message);
        }
    }
}
