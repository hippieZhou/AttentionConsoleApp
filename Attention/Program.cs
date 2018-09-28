using Attention.BLL.Clients;
using Attention.BLL.Services;
using Attention.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Attention
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json");

            builder.AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<BingClient>();

            services.AddDbContext<AttentionDbContext>(options => { options.UseSqlite(configuration.GetConnectionString("DefaultConnection")); });

            services.AddSingleton<BingService>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("Hello World!");

            BingService bingService = serviceProvider.GetService<BingService>();

            var models = await bingService.GetAllBingsAsync();
            var list = models.OrderBy(p => p.Startdate);
            foreach (var model in list)
            {
                Console.WriteLine($"{model.Id} - {model.Startdate} - {model.Enddate}");
            }
            Console.ReadKey();
        }
    }
}
