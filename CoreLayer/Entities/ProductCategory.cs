using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Product> Products { get; } = new List<Product>();

    }
}
