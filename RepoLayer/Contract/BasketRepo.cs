using CoreLayer.Entities;
using CoreLayer.RepoContract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepoLayer.Contract
{
    public class BasketRepo : IBasketRepo
    {
        public IDatabase _Db { get; set; }
        public BasketRepo(IConnectionMultiplexer redis)
        {
            _Db = redis.GetDatabase();

        }
        public Task<bool> DeleteBasket(string id)
        {

           return _Db.KeyDeleteAsync(id);
        }

        public async Task<Basket?> GetById(string id)
        {
            var basket =await  _Db.StringGetAsync(id);
          return basket.IsNull ? null : JsonSerializer.Deserialize<Basket>(_Db.StringGet(id));

        }

        public async Task<Basket?> UpdateBasket(Basket basket)
        {
            var addedbasket = JsonSerializer.Serialize(basket);
            var result = await _Db.StringSetAsync(basket.id, addedbasket, TimeSpan.FromDays(1));
            if (!result) return null;
            return await GetById(basket.id);
        }
    }
}
