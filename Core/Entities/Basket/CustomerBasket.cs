using System.Collections.Generic;

namespace Core.Entities.Basket
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
            // we need an empty constructor for redis
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}