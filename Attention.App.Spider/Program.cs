using Attention.BLL.Clients;
using Attention.BLL.Services;
using Attention.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Attention.App.Spider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<BingClient>();

            services.AddDbContext<AppDbContext>(options => { options.UseSqlite(configuration.GetConnectionString("DefaultConnection")); });

            services.AddSingleton<BingService>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("Hello World!");

            //BingService bingService = serviceProvider.GetService<BingService>();
            //await MigrationAsync(bingService);

            BingClient bingClient = serviceProvider.GetService<BingClient>();
            await SpiderAsync(bingClient);

            Console.ReadKey();
        }

        private static async Task MigrationAsync(BingService bingService )
        {
            await bingService.MigrationAsync();
        }

        private static async Task SpiderAsync(BingClient client)
        {
            await client.GetBingModelsAsync();
        }
    }
}
