using Attention.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.BLL.Models
{
    public class BingModel : Bing
    {
        public BingModel(Bing model)
        {
            Id = model.Id;
            Hsh = model.Hsh;
            DateTime = model.DateTime;
            Url = model.Url;
            UrlBase = model.UrlBase;
            Copyright = model.Copyright;
            Title = model.Title;
            Caption = model.Caption;
            Description = model.Description;
            Shares = model.Shares;
            Likes = model.Likes;
        }
    }
}
