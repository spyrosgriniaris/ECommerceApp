using System.Threading.Tasks;
using Core.Entities.Basket;

namespace Core.Interfaces.Basket
{
    public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string basketId);
         Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
         Task<bool> DeleteBasketAsync(string basketId);
    }
}