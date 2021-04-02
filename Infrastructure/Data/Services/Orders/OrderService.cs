using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Interfaces.Basket;
using Core.Interfaces.Orders;
using Core.Interfaces.UnitOfWork;
using Core.Specifications.Orders;

namespace Infrastructure.Data.Services.Orders
{
    public class OrderService : IOrderService
    {
        // private readonly IGenericRepository<Order> _orderRepo;
        // private readonly IGenericRepository<Product> _productRepo;
        // private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;
        public OrderService(IUnitOfWork unitOfWork,// IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo, IGenericRepository<Product> productRepo,
            IBasketRepository basketRepo)
        {
            // removed in order to use the unit of work
            // _dmRepo = dmRepo;
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
            // _productRepo = productRepo;
            // _orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from basketrepo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get items from the product repo
            var items = new List<OrderItem>();
            foreach(var item in basket.Items) {
                // var productItem = await _productRepo.GetByIdAsync(item.Id);
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);// after unit of work
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            // get delivery method from repo
            // var deliveryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subTotal = items.Sum(item => item.Price * item.Quantity);

            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subTotal);

            //TODO save to db
            // unit of work guarantees that all the changes will take place else none of them
            _unitOfWork.Repository<Order>().Add(order);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            // return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}