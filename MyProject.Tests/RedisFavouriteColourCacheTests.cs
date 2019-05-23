using MyProject.Business;
using System;
using Xunit;

namespace MyProject.Tests
{
    public class RedisFavouriteColourCacheTests
    {
        protected RedisFavouriteColourCache GetCache()
        {
            RedisFavouriteColourCacheOptions options = new RedisFavouriteColourCacheOptions
            {
                RedisConnectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS_REDIS") ?? "localhost:6379"
            };
            return new RedisFavouriteColourCache(options);
        }

        [Fact]
        public void Test_Can_Get_Colour_If_Already_Set()
        {
            // ARRANGE
            RedisFavouriteColourCache cache = this.GetCache();

            // ACT
            cache.StoreFavouriteColour("John", "Blue");
            cache.StoreFavouriteColour("Mary", "Red");

            string johnsFavouriteColour = cache.RetrieveFavouriteColour("John");
            string marysFavouriteColour = cache.RetrieveFavouriteColour("Mary");

            // ASSERT
            Assert.Equal("Blue", johnsFavouriteColour);
            Assert.Equal("Red", marysFavouriteColour);
        }

        [Fact]
        public void Test_Can_Change_Colour()
        {
            // ARRANGE
            RedisFavouriteColourCache cache = this.GetCache();

            // ACT
            cache.StoreFavouriteColour("John", "Blue");
            cache.StoreFavouriteColour("John", "Red");

            string johnsFavouriteColour = cache.RetrieveFavouriteColour("John");

            // ASSERT
            Assert.Equal("Red", johnsFavouriteColour);
        }

        [Fact]
        public void Test_Null_Returned_If_Favourite_Colour_Not_Set()
        {
            // ARRANGE
            RedisFavouriteColourCache cache = this.GetCache();

            // ACT
            cache.ClearFavouriteColour("John");
            string johnsFavouriteColour = cache.RetrieveFavouriteColour("John");

            // ASSERT
            Assert.Null(johnsFavouriteColour);
        }
    }
}
