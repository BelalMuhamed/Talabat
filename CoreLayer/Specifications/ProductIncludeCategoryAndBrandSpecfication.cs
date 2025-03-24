using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Specifications
{
   public  class ProductIncludeCategoryAndBrandSpecfication:BaseSpecifiction<Product>
    {
        public ProductIncludeCategoryAndBrandSpecfication(SpecProductParams Params):
            base(b => (!Params.brandid.HasValue || b.BrandId==Params.brandid)&&(!Params.categoryid.HasValue || b.CategoryId== Params.categoryid) && (string.IsNullOrEmpty(Params.SearchByName) || b.Name.ToLower().Contains(Params.SearchByName)))
        {
            AddIncludes();
            if(Params.sort != null )
            {
              switch(Params.sort)
                {
                    case "price":
                        orderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        orderByDesc(p => p.Price);
                        break;
                    default:
                        orderBy(p => p.Id);
                        break;
                }
            }
            Pagination(Params.PageSize*(Params.PageIndex -1) , Params.PageSize);
        }
        public ProductIncludeCategoryAndBrandSpecfication(Expression<Func<Product, bool>>? inputCriteria):base(inputCriteria)
        {

            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
