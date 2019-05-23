using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace MyProject.Business
{
    public class RedisFavouriteColourCache : IFavouriteColourCache
    {
        public RedisFavouriteColourCache(IOptions<RedisFavouriteColourCacheOptions> options)
        {
            this.Redis = ConnectionMultiplexer.Connect(options.Value.RedisConnectionString);
        }

        protected ConnectionMultiplexer Redis { get; }

        public string RetrieveFavouriteColour(string userName)
        {
            return this.Redis.GetDatabase().StringGet(userName);
        }

        public void StoreFavouriteColour(string userName, string colour)
        {
            this.Redis.GetDatabase().StringSet(userName, colour);
        }

        public void ClearFavouriteColour(string userName)
        {
            this.Redis.GetDatabase().KeyDelete(userName);
        }
    }
}
