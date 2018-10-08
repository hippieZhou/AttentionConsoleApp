using Attention.BLL.Clients;
using Attention.BLL.Models;
using Attention.DAL;
using Attention.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<List<HippieModel>> GetAllHippiesAsync()
        {
            List<HippieModel> hippies = await dbContext.Hippies
                .OrderByDescending(p => p.DateTime)
                .Select(p => new HippieModel(p))
                .ToListAsync();

            if (hippies == null || hippies?.FirstOrDefault()?.DateTime < DateTime.Now)
            {
                var today = await bingClient.GetBingModelsAsync();
                foreach (var item in today.Images)
                {
                    Hippie model = new Hippie
                    {
                        Hsh = item.Hsh,
                        DateTime = DateTime.ParseExact(item.Startdate, "yyyyMMdd", null),
                        UrlBase = item.Urlbase,
                        Copyright = item.Copyright,
                        Title = item.Title,
                        Caption = item.Caption,
                        Description = item.Desc,
                        Shares = 0,
                        Likes = 0
                    };
                    Hippie has = hippies.FirstOrDefault(p => p.DateTime == model.DateTime);
                    if (has == null)
                    {
                        dbContext.Hippies.Add(model);
                    }
                }
                dbContext.SaveChanges();
            }

            List<HippieModel> hippieModels = await dbContext.Hippies
                .OrderByDescending(p => p.DateTime)
                .Select(p => new HippieModel(p))
                .ToListAsync();

            return hippieModels;
        }

        public async Task MigrationAsync()
        {
            await Task.Delay(1000);
        }
    }
}
