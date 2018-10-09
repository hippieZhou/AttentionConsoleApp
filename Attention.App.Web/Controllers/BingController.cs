using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Attention.App.Web.Models;
using Attention.BLL.Models;
using Attention.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Attention.App.Web.Controllers
{
    public class BingController : Controller
    {
        public BingService BingService { get; }

        public BingController(BingService service)
        {
            BingService = service;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var bings = await BingService.GetAllBingsAsync();

            PaginatedList<BingModel> list = PaginatedList<BingModel>.Create(bings.AsQueryable().AsNoTracking(), page ?? 1, 10);
            return View(list);
        }

        public async Task<IActionResult> Detail(string hsh)
        {
            var bing = await BingService.GetBingByHshAsync(hsh);
            return View(bing);
        }
    }
}