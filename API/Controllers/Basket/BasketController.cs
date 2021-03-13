using System.Threading.Tasks;
using API.DTOs.BasketDTOs;
using AutoMapper;
using Core.Entities.Basket;
using Core.Interfaces.Basket;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Basket
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketId([FromBody]string basketId) 
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
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