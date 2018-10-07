using Attention.BLL.Clients;
using Attention.BLL.Models;
using Attention.BLL.Utils;
using Attention.DAL;
using Attention.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly ApplicationDbContext dbContext;

        public BingService(BingClient httpClient, ApplicationDbContext attentionDbContext)
        {
            bingClient = httpClient;
            dbContext = attentionDbContext;
        }

        public async Task<List<Models.BingModel>> GetAllBingsAsync()
        {
            List<Models.BingModel> olds = await dbContext.Bings.OrderByDescending(p => p.Enddate).Select(
               s => (Models.BingModel)s.ConvertToBingModel()
             ).ToListAsync();

            Models.BingModel first = olds.FirstOrDefault();
            if (first == null || first?.Enddate < DateTime.Now)
            {
                var today = await bingClient.GetBingModelsAsync();
                foreach (var item in today.Images)
                {
                    Models.BingModel model = item.ConvertToBingModel();
                    Models.BingModel has = olds.FirstOrDefault(p => p.Startdate.Date == model.Startdate.Date);
                    if (has == null)
                    {
                        await InsertBingAsync(model.ConvertToBingModel());
                    }
                }
                dbContext.SaveChanges();
            }

            List<Models.BingModel> bings = await dbContext.Bings.Where(p => p.Enddate <= DateTime.Now)
                .OrderByDescending(p => p.Enddate)
                .Select(s => (Models.BingModel)s.ConvertToBingModel())
                .ToListAsync();

            return bings;
        }

        public async Task<Models.BingModel> GetBingByIdAsync(int id)
        {
            return await dbContext.Bings.Select(
                s => s.ConvertToBingModel())
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateBingAsync(Models.BingModel model)
        {
            var entity = await dbContext.Bings.FirstOrDefaultAsync(s => s.Id == model.Id);

            entity = model.ConvertToBingEntity();

            await dbContext.SaveChangesAsync();
        }

        public async Task InsertBingAsync(Models.BingModel model)
        {
            var entity = model.ConvertToBingEntity();

            dbContext.Bings.Add(entity);

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var entity = new Bing()
            {
                Id = id
            };
            dbContext.Bings.Attach(entity);
            dbContext.Bings.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        [Obsolete]
        public void Save()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "history.json");
            string[] jsons = File.ReadAllLines(path);
            for (int i = 0; i < jsons.Length; i++)
            {
                try
                {
                    var current = JsonConvert.DeserializeObject<RootObject>(jsons[i]);
                    string url = current.Url.Split(new[] { '/', '?' })[4];
                    var model = new Models.BingModel
                    {
                        Caption = current.Title,
                        Startdate = DateTime.ParseExact(current.Date, "yyyy-MM-dd", null),
                        Copyright = current.Title,
                        Url = $"https://cn.bing.com/az/hprichbg/rb/{url}__1920x1080.jpg",
                        Urlbase = $"https://cn.bing.com/az/hprichbg/rb/{url}",
                        Title = current.Locate,
                        Desc = current.Description,
                        Hsh = current.Id
                    };
                    model.Enddate = model.Startdate.AddDays(1);

                    dbContext.Bings.Add(model);
                }
                catch (Exception)
                {
                    continue;
                }
                finally
                {
                    dbContext.SaveChanges();
                }
            }
        }

        public void Todo()
        {
            var list = dbContext.Bings.OrderBy(p => p.Startdate);
            var items = new List<Bing>(list);
            dbContext.Database.ExecuteSqlCommand("delete from BING");
            dbContext.SaveChanges();
            foreach (var item in items)
            {
                Console.WriteLine(item.Startdate);
                dbContext.Bings.Add(item);
                dbContext.SaveChanges();
            }
        }
    }

    public class RootObject
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("locate")]
        public string Locate { get; set; }
        [JsonProperty("updateTime")]
        public string UpdateTime { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("addDateTime")]
        public string AddDateTime { get; set; }
    }

}
