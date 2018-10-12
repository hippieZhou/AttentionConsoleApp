using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Attention.Models;
using Attention.BLL.Services;
using Attention.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Attention.Controllers
{
    public class HomeController : Controller
    {
        public BingService BingService { get; }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public HomeController(BingService service)
        {
            BingService = service;
        }

        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> Index(int? page)
        {
            var bings = await BingService.GetAllBingsAsync();

            PaginatedList<BingModel> list = PaginatedList<BingModel>.Create(bings.AsQueryable().AsNoTracking(), page ?? 1, 10);
            return View(list);
        }

        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> Detail(string hsh)
        {
            var bing = await BingService.GetBingByHshAsync(hsh);
            return View(bing);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
