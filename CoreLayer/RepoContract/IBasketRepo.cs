using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.RepoContract
{
  public  interface IBasketRepo
    {
        public Task<Basket?> GetById(string id);
        public Task<Basket?> UpdateBasket(Basket basket);
        public Task<bool> DeleteBasket(string id);
    }
}
