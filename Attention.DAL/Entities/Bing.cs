using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Attention.DAL.Entities
{
    [Table("BING")]
    public class Bing
    {
        [Key]
        public int Id { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Url { get; set; }
        public string Urlbase { get; set; }
        public string Copyright { get; set; }
        public string Copyrightlink { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Copyrightonly { get; set; }
        public string Desc { get; set; }
        public string Quiz { get; set; }
        public string Hsh { get; set; }
    }
}
