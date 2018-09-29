using Attention.DAL;
using Attention.DAL.Entities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Attention.Spider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Spider spider = new Spider();
            await spider.RunAsync();

            Console.ReadKey();
        }
    }

    public class Spider
    {
        private static readonly string BASE_ADDRESS = "https://bing.ioliu.cn";
        private static readonly HttpClient _httpClient;
        static Spider()
        {
            //https://bing.ioliu.cn/?p=4
            //_httpClient = new HttpClient() { BaseAddress = new Uri(BASE_ADDRESS) };
            //_httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            //_httpClient.SendAsync(new HttpRequestMessage
            //{
            //    Method = new HttpMethod("HEAD"),
            //    RequestUri = new Uri(BASE_ADDRESS)
            //}).Result.EnsureSuccessStatusCode();
        }

        public async Task RunAsync()
        {
            var connection = @"Data Source=default.db";
            var optionsBuilder = new DbContextOptionsBuilder<AttentionDbContext>();
            optionsBuilder.UseSqlite(connection);

            using (var dbContext = new AttentionDbContext(optionsBuilder.Options))
            {
                HtmlWeb web = new HtmlWeb();

                for (int i = 0; i < 79; i++)
                {
                    var doc = await web.LoadFromWebAsync($"{BASE_ADDRESS}/?p={i + 1}");
                    var items = doc.DocumentNode.SelectNodes("//div[@class='item']");
                    for (int j = 0; j < items.Count; j++)
                    {
                        var current = items[j];
                        var img = current.SelectSingleNode("//img");
                        var desc = current.SelectSingleNode("//h3");
                        var date = current.SelectSingleNode("//div[@class='calendar']");
                        var location = current.SelectSingleNode("//div[@class='location']");

                        //dbContext.Iolius.Add(new Ioliu
                        //{

                        //});
                    }
                }
            }
        }
    }
}
