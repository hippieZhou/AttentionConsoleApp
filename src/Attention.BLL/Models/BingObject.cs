using Attention.DAL.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.BLL.Models
{
//    resolutions: [
//    '1920x1200',
//    '1920x1080',
//    '1366x768',
//    '1280x768',
//    '1024x768',
//    '800x600',
//    '800x480',
//    '768x1280',
//    '720x1280',
//    '640x480',
//    '480x800',
//    '400x240',
//    '320x240',
//    '240x320'
//]
    public class BingObject
    {
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
        [JsonProperty("tooltips")]
        public Tooltips Tooltips { get; set; }
        [JsonProperty("quiz")]
        public Quiz Quiz { get; set; }
    }
    public class Image
    {
        [JsonProperty("startdate")]
        public string Startdate { get; set; }
        [JsonProperty("fullstartdate")]
        public string Fullstartdate { get; set; }
        [JsonProperty("enddate")]
        public string Enddate { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("urlbase")]
        public string Urlbase { get; set; }
        [JsonProperty("copyright")]
        public string Copyright { get; set; }
        [JsonProperty("copyrightlink")]
        public string Copyrightlink { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("copyrightonly")]
        public string Copyrightonly { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("bsTitle")]
        public string BsTitle { get; set; }
        [JsonProperty("quiz")]
        public string Quiz { get; set; }
        [JsonProperty("wp")]
        public bool Wp { get; set; }
        [JsonProperty("hsh")]
        public string Hsh { get; set; }
        [JsonProperty("drk")]
        public int Drk { get; set; }
        [JsonProperty("top")]
        public int Top { get; set; }
        [JsonProperty("bot")]
        public int Bot { get; set; }
        [JsonProperty("hs")]
        public List<object> Hs { get; set; }
        [JsonProperty("og")]
        public Og Og { get; set; }
    }
    public class Og
    {
        [JsonProperty("img")]
        public string Img { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
    public class Tooltips
    {
        [JsonProperty("loading")]
        public string Loading { get; set; }
        [JsonProperty("previous")]
        public string Previous { get; set; }
        [JsonProperty("next")]
        public string Next { get; set; }
        [JsonProperty("walle")]
        public string Walle { get; set; }
        [JsonProperty("walls")]
        public string Walls { get; set; }
    }
    public class Quiz
    {
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("options")]
        public List<Option> Options { get; set; }
    }
    public class Option
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
