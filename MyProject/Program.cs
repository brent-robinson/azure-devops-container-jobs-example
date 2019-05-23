using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Business;
using System;
using System.IO;

namespace MyProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Get application configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Build dependency injection context
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.Configure<RedisFavouriteColourCacheOptions>(o => o.RedisConnectionString = configuration.GetConnectionString("Redis"));
            serviceCollection.AddSingleton<IFavouriteColourCache, RedisFavouriteColourCache>();
            IServiceProvider services = serviceCollection.BuildServiceProvider();

            // Validate arguments
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("Enter a name, or a name and colour");
                return;
            }

            // Get the colour cache service
            IFavouriteColourCache cache = services.GetRequiredService<IFavouriteColourCache>();

            // Get the favourite colour of the person
            if (args.Length == 1)
            {
                Console.WriteLine(cache.RetrieveFavouriteColour(args[0]));
            }
            // Set the favourite colour of the person
            else if (args.Length == 2)
            {
                cache.StoreFavouriteColour(args[0], args[1]);
            }
        }
    }
}
