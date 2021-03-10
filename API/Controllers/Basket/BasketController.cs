using System.Threading.Tasks;
using Core.Entities.Basket;
using Core.Interfaces.Basket;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Basket
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketId([FromBody]string basketId) 
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket)
        {
            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string basketId) 
        {
            await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}