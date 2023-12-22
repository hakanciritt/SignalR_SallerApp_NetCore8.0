using StackExchange.Redis;

namespace SignalR_App.Application.Redis
{
    public class RedisConfiguration(ConnectionMultiplexer connectionMultiplexer)
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer = connectionMultiplexer;

        public IDatabase Connect(int databaseId = 0)
        {
            var database = _connectionMultiplexer.GetDatabase(databaseId);
            return database;
        }

        public ConnectionMultiplexer ConnectionMultiplexer => _connectionMultiplexer;

    }
}
