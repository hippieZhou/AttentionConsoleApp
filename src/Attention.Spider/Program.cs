using Attention.BLL.Models;
using Attention.DAL;
using Attention.DAL.Entities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Attention.Spider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=default.db");

            HtmlWeb web = new HtmlWeb();
            for (int i = 3; i < 6; i++)
            {
                var url = $"https://bing.ioliu.cn/?p={i}";
                HtmlDocument doc = await web.LoadFromWebAsync(url);

                HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("//div[@class='card progressive']");
                if (htmlNodes == null)
                    continue;

                foreach (var htmlNode in htmlNodes)
                {
                    //https://bing.ioliu.cn/photo/ShenandoahAutumn_EN-AU11784755049?force=home_3

                    var mark = $"https://bing.ioliu.cn{htmlNode.SelectSingleNode("./a[@class='mark']").GetAttributeValue("href", "")}";
                    //Console.WriteLine(mark);

                    HtmlDocument dayNode = await web.LoadFromWebAsync(mark);
                    var src = dayNode.DocumentNode.SelectSingleNode("//img").GetAttributeValue("data-progressive", "");
                    var copyright = dayNode.DocumentNode.SelectSingleNode("//p[@class='title']").InnerText.Trim();
                    var sub = dayNode.DocumentNode.SelectSingleNode("//p[@class='sub']").InnerText.Trim();
                    var calendar = dayNode.DocumentNode.SelectSingleNode("//p[@class='calendari icon icon-calendar']").InnerText.Trim();
                    var location = dayNode.DocumentNode.SelectSingleNode("//p[@class='location']").InnerText.Trim();

                    var model = new Bing
                    {
                        Hsh = "---------------------",
                        DateTime = DateTime.ParseExact(calendar, "yyyy-MM-dd", null),
                        Url = src,
                        UrlBase = $"https://cn.bing.com/az/hprichbg/rb{ src.Split(new[] { '/' }).LastOrDefault()}",
                        Copyright = copyright,
                        Caption = copyright,
                        Description = sub,
                        Title = location,
                    };
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    //Console.WriteLine(json);
                    if (model.DateTime > new DateTime(2018, 8, 20) && model.DateTime < new DateTime(2018, 9, 14))
                    {
                        using (var dbContext = new AppDbContext(optionsBuilder.Options))
                        {
                            dbContext.Bings.Add(model);
                            dbContext.SaveChanges();
                            Console.WriteLine("数据导入成功");
                        }
                    }
                    Console.WriteLine(model.DateTime);
                }
            }
            Console.WriteLine("完成");
            Console.ReadKey();
        }
    }
}
