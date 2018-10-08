using Attention.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.BLL.Models
{
    public class HippieModel : Hippie
    {
        public string ImageUrl_1920x1080 { get; private set; }

        public HippieModel(Hippie hippie)
        {
            Id = hippie.Id;
            Hsh = hippie.Hsh;
            DateTime = hippie.DateTime;
            UrlBase = hippie.UrlBase;
            Copyright = hippie.Copyright;
            Title = hippie.Title;
            Caption = hippie.Caption;
            Description = hippie.Description;
            Shares = hippie.Shares;
            Likes = hippie.Likes;

            ImageUrl_1920x1080 = $"{UrlBase}_1920x1080.jpg";
        }
    }
}
