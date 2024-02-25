using grPcExample.Context;
using grPcExample.Models;

namespace grPcExample.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> AddOrder(Order order)
        {
            if (order is not null)
            {
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
            }
            return order!;
        }

        public IEnumerable<Order> AllOrders()
        {
            return _dbContext.Orders.OrderByDescending(c => c.CreatedAt).ToList();
        }

        public bool DeleteOrderById(string Id)
        {
            var orderObject = _dbContext.Orders.FirstOrDefault(c => c.Id == Id);
            if (orderObject is not null)
            {
                _dbContext.Orders.Remove(orderObject);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Order> EditOrder(Order order)
        {
            var orderObject = _dbContext.Orders.FirstOrDefault(c => c.Id == order.Id);
            if (orderObject is not null)
            {
                orderObject.Name = order.Name;
                orderObject.Enable = order.Enable;
                await _dbContext.SaveChangesAsync();
            }
            return order;
        }

        public Order GetOrderById(string Id) => _dbContext.Orders.FirstOrDefault(c => c.Id == Id)!;
    }
}
