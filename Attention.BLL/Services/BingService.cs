using Attention.BLL.Clients;
using Attention.BLL.Models;
using Attention.DAL;
using Attention.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Attention.BLL.Services
{
    public class BingService
    {
        private readonly BingClient bingClient;
        private readonly AppDbContext dbContext;

        public BingService(BingClient httpClient, AppDbContext attentionDbContext)
        {
            bingClient = httpClient;
            dbContext = attentionDbContext;
        }

        public async Task<List<BingModel>> GetAllBingsAsync()
        {
            List<BingModel> bings = await dbContext.Bings
                .OrderByDescending(p => p.DateTime)
                .Select(p => new BingModel(p))
                .ToListAsync();

            if (bings == null || bings?.FirstOrDefault()?.DateTime < DateTime.Now)
            {
                var today = await bingClient.GetBingModelsAsync();
                foreach (var item in today.Images)
                {
                    Bing model = new Bing
                    {
                        Hsh = item.Hsh,
                        DateTime = DateTime.ParseExact(item.Startdate, "yyyyMMdd", null),
                        Url = item.Url,
                        UrlBase = item.Urlbase,
                        Copyright = item.Copyright,
                        Title = item.Title,
                        Caption = item.Caption,
                        Description = item.Desc,
                        Shares = 0,
                        Likes = 0
                    };
                    Bing has = bings.FirstOrDefault(p => p.DateTime == model.DateTime);
                    if (has == null)
                    {
                        dbContext.Bings.Add(model);
                    }
                }
                dbContext.SaveChanges();
            }

            List<BingModel> bingModels = await dbContext.Bings
                .OrderByDescending(p => p.DateTime)
                .Select(p => new BingModel(p))
                .ToListAsync();

            return bingModels;
        }

        public async Task<BingModel> GetBingByHshAsync(string hsh)
        {
            var bing = await dbContext.Bings.FirstOrDefaultAsync(p => p.Hsh == hsh);
            return new BingModel(bing);
        }

        public async Task<byte[]> DownLoadImageAsync(string url)
        {
            byte[] fileBytes = await bingClient.DownLoadImageAsync(url);
            return fileBytes;
        }

        public async Task MigrationAsync()
        {
            //var strs = await bingClient.GetBingModelsAsync();
            //var bings = await dbContext.Bings.ToListAsync();
            //foreach (var bing in bings)
            //{
            //    bing.Url = $"{bing.UrlBase}_1920x1080.jpg";
            //    Console.WriteLine(bing.DateTime);
            //}
            //dbContext.SaveChanges();

            var json = Path.Combine(AppContext.BaseDirectory, "history.json");
            string[] lines = File.ReadAllLines(json);
            foreach (var line in lines)
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(line);
                string hsh = model.GetValue("_id").ToString();
                string url = model.GetValue("url").ToString();
                if (string.IsNullOrWhiteSpace(hsh) || string.IsNullOrWhiteSpace(url))
                    continue;

                //https://cn.bing.com/az/hprichbg/rb/ElephantParade_EN-AU11671803284_1920x1080.jpg
                //http://h1.ioliu.cn/bing/SkylineparkRoller_EN-AU8492771279_1920x1080.jpg
                //string img = "https://cn.bing.com/az/hprichbg/rb/" + url.Split(new[] { '/', '?' })[4] + "_1920x1080.jpg";
                string img = "http://h1.ioliu.cn/bing/" + url.Split(new[] { '/', '?' })[4] + "_1920x1080.jpg";

                var bing = dbContext.Bings.FirstOrDefault(p => p.Hsh == hsh);
                if (bing != null)
                {
                    bing.Url = img;
                    Console.WriteLine(bing.Url);
                    dbContext.SaveChanges();
                }
                await Task.Delay(100);
            }
        }
    }
}
