using Attention.BLL.Models;
using Attention.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.BLL.Utils
{
    public static class BingExtension
    {
        public static BingModel ConvertToBingModel(this Bing model)
        {
            return new Models.BingModel
            {
                Id = model.Id,
                Startdate = model.Startdate,
                Enddate = model.Enddate,
                Url = model.Url,
                Urlbase = model.Urlbase,
                Copyright = model.Copyright,
                Copyrightlink = model.Copyrightlink,
                Copyrightonly = model.Copyrightonly,
                Title = model.Title,
                Caption = model.Caption,
                Desc = model.Desc,
                Quiz = model.Quiz,
                Hsh = model.Hsh,
            };
        }
        public static Bing ConvertToBingEntity(this Models.BingModel model)
        {
            return new Bing
            {
                Id = model.Id,
                Startdate = model.Startdate,
                Enddate = model.Enddate,
                Url = model.Url,
                Urlbase = model.Urlbase,
                Copyright = model.Copyright,
                Copyrightlink = model.Copyrightlink,
                Title = model.Title,
                Caption = model.Caption,
                Copyrightonly = model.Copyrightonly,
                Desc = model.Desc,
                Quiz = model.Quiz,
                Hsh = model.Hsh
            };
        }
        public static BingModel ConvertToBingModel(this Image model)
        {
            return new Models.BingModel
            {
                Startdate = DateTime.ParseExact(model.Startdate, "yyyyMMdd", null),
                Enddate = DateTime.ParseExact(model.Enddate, "yyyyMMdd", null),
                Url = model.Url,
                Urlbase = model.Urlbase,
                Copyright = model.Copyright,
                Copyrightlink = model.Copyrightlink,
                Copyrightonly = model.Copyrightonly,
                Title = model.Title,
                Caption = model.Caption,
                Desc = model.Desc,
                Quiz = model.Quiz,
                Hsh = model.Hsh,
            };
        }
    }
}
