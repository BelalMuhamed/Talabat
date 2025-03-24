using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
  public  class Basket
    {
        public string id { get; set; }
        public List<Basketitem> basketItems { get; set; } = new();
    }
}
