﻿using System;
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
using System.IO;

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

        public async Task<IActionResult> Index(int? page)
        {
            var bings = await BingService.GetAllBingsAsync();

            PaginatedList<BingModel> list = PaginatedList<BingModel>.Create(bings.AsQueryable().AsNoTracking(), page ?? 1, 10);
            return View(list);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var bing = await BingService.GetBingByIdAyncs(id);
            return View(bing);
        }

        public async Task<IActionResult> Download(string url)
        {
            FileInfo info = new FileInfo(url);
            string name = info.Name;
            byte[] fileBytes = await BingService.DownLoadImageAsync(url);
            return File(fileBytes, "application/x-msdownload", name);
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
