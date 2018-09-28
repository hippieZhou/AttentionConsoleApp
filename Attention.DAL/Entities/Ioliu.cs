using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Attention.DAL.Entities
{
    [Table("IOLIU")]
    public class Ioliu
    {
        [Key]
        public int Id { get; set; }
        public string Hsh { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public int Shares { get; set; }
        public int Likes { get; set; }
    }
}
