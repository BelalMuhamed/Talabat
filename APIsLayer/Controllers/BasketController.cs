using APIsLayer.Errors;
using CoreLayer.Entities;
using CoreLayer.RepoContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Contract;
using System.Threading.Tasks;

namespace APIsLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepo repo;

        public BasketController(IBasketRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<Basket>> GetOrRecreate(string id)
        {
            var basket =  repo.GetById(id).Result;
            return Ok(basket ?? new Basket { id = id });
        }
        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateOrCreate(Basket basket)
        {
            var Updatedbasket =await repo.UpdateBasket(basket);
            if(Updatedbasket == null)
            {
                return BadRequest(new APIResponse(400));
            }
            return Ok(Updatedbasket);
        }
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
           var result=await repo.DeleteBasket(id);
            return result;
        }
    }
}
