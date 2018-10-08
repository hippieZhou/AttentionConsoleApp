using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attention.DAL.Entities
{
    [Table("BING")]
    public class Bing
    {
        [Key]
        public int Id { get; set; }
        public string Hsh { get; set; }
        public DateTime DateTime { get; set; }
        public string UrlBase { get; set; }
        public string Copyright { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }

        public int Shares { get; set; }
        public int Likes { get; set; }
    }
}
