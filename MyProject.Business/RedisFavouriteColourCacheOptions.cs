using Microsoft.Extensions.Options;

namespace MyProject.Business
{
    public class RedisFavouriteColourCacheOptions : IOptions<RedisFavouriteColourCacheOptions>
    {
        public RedisFavouriteColourCacheOptions Value => this;

        public string RedisConnectionString { get; set; }
    }
}
