using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Identity
{
   public class Address
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string city{ get; set; }
        public string street { get; set; }
        public string country { get; set; }
        public  string userid { get; set; }
        [ForeignKey(nameof(userid))]
        public virtual UserApplication user { get; set; }
    }
}
