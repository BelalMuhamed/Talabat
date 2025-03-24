using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Specifications
{
   public class SpecPaginationCountForSpec:BaseSpecifiction<Product>
    {
        public SpecPaginationCountForSpec(SpecProductParams Params) :
            base(b => (!Params.brandid.HasValue || b.BrandId == Params.brandid) && (!Params.categoryid.HasValue || b.CategoryId == Params.categoryid) && (string.IsNullOrEmpty(Params.SearchByName) || b.Name.ToLower().Contains(Params.SearchByName)))
        {
            
        }
    }
}
