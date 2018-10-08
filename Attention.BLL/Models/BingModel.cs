using Attention.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.BLL.Models
{
    public class BingModel : Bing
    {
        public string ImageUrl_1920x1080 { get; private set; }

        public BingModel(Bing model)
        {
            Id = model.Id;
            Hsh = model.Hsh;
            DateTime = model.DateTime;
            UrlBase = model.UrlBase;
            Copyright = model.Copyright;
            Title = model.Title;
            Caption = model.Caption;
            Description = model.Description;
            Shares = model.Shares;
            Likes = model.Likes;

            ImageUrl_1920x1080 = $"{UrlBase}_1920x1080.jpg";
        }
    }
}
