using Microsoft.EntityFrameworkCore;
using Product.Core.Entities.Orders;
using Product.Core.Interface;
using Product.Infrastrucre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastrucre.Repository
{   
    public class OrderServices : IOrderServices
    {
        private readonly IUnitWork _uow;
        private readonly ApplicationDbContext _context;

        public OrderServices(IUnitWork uow, ApplicationDbContext context)
        {
            _context = context;
            _uow = uow;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShipAddress shipAddress)
        {
            //Get basket items
            var basket = await _uow.BasketRepository.GetBasketAsync(basketId);
            var items = new List<OrderItems>();
            foreach(var item in basket.BasketItems)
            {
                var productItem = await _uow.ProductRepository.GetByIdAsync(item.Id);
                var productItemOrder = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.ProductPicture);
                var OrderItem = new OrderItems(productItemOrder, item.Price, item.Quantity);
                items.Add(OrderItem);
            }
            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            //Dekivery Method
            var deliverymethod = await _context.DeliveryMethods.Where(x => x.Id == deliveryMethodId).FirstOrDefaultAsync();
            //Caculate subtotal
            var subtotal = items.Sum(x => x.price*x.quantity);

            var order = new Order(buyerEmail, shipAddress, deliverymethod, items, subtotal);
            //checkout null
            if (order is null) return null;

            //addnig Order DB 
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            //Remove basket after save order in database
            await _uow.BasketRepository.DeleteBasketAsync(basketId);
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        => await _context.DeliveryMethods.ToListAsync();

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _context.Orders
                 .Where(x => x.OrderId == id && x.BuyerEmail == buyerEmail)
                 .Include(x => x.orderItems).ThenInclude(x => x.productItemOrdered)
                 .Include(x => x.DeliveryMethod).FirstOrDefaultAsync();
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var order = await _context.Orders
                .Where(x => x.BuyerEmail == buyerEmail)
                .Include(x => x.orderItems).ThenInclude(x => x.productItemOrdered)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
            return order;
        }
    }
}
