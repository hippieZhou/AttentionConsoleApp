using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Attention.App.Web.Models;
using Attention.BLL.Services;
using Attention.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace Attention.App.Web.Controllers
{
    public class HomeController : Controller
    {
        public BingService BingService { get; }

        public HomeController(BingService service)
        {
            BingService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Bing(int? page)
        {
            Trace.WriteLine(page);
            var bings = await BingService.GetAllBingsAsync();

            PaginatedList<BingModel> list = PaginatedList<BingModel>.Create(bings.AsQueryable().AsNoTracking(), page ?? 1, 10);
            return View(list);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
