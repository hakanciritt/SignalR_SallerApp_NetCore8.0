using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SignalR_App.Application.Redis;
using StackExchange.Redis;

namespace SignalR_App.Application.Hubs
{
    public class MainHub : Hub
    {
        private readonly RedisConfiguration _redis;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MainHub(RedisConfiguration redis, IHttpContextAccessor httpContextAccessor)
        {
            _redis = redis;
            _httpContextAccessor = httpContextAccessor;
        }
        public override async Task OnConnectedAsync()
        {
            var connection = _redis.Connect(0);
            var address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var list = (await connection.ListRangeAsync("ConnectionList")).ToStringArray();
            if (!list.Any(d => d == address)) await connection.ListRightPushAsync("ConnectionList", address);
            int connectionCount = (await connection.ListRangeAsync("ConnectionList")).Count();
            await Clients.All.SendAsync(ReceiverNames.ReceiveConnectionCount, connectionCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connection = _redis.Connect(0);
            var address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var list = (await connection.ListRangeAsync("ConnectionList")).ToStringArray();
            if (list.Any(c => c == address)) await connection.ListRemoveAsync("ConnectionList", address);
            int connectionCount = (await connection.ListRangeAsync("ConnectionList")).Count();
            await Clients.All.SendAsync(ReceiverNames.ReceiveConnectionCount, connectionCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
