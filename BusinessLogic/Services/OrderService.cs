using BusinessLogic.Contracts;
using BusinessLogic.Data;
using BusinessLogic.Models;
using BusinessLogic.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Order>> GetOrders(string userName)
        {
            return _context.Orders
                // Include veranlasst ein Join auf der DB
                .Include(x => x.OrderItems)
                .Where(x => x.UserName == userName).ToListAsync();
        }

        public Task<Order?> GetOrderById(int id)
        {
            return _context.Orders
                // Include veranlasst ein Join auf der DB
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Recipe)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order?> GetPendingOrderByUserId(string userName)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Recipe)
                .FirstOrDefaultAsync(x => x.UserName == userName && x.Status == OrderStatus.Pending);
        }

        public async Task<Order> CurrentOrder(string userName)
        {
            Order? order = await GetPendingOrderByUserId(userName);

            if (order == null)
            {
                order = new Order
                {
                    UserName = userName,
                    Status = OrderStatus.Pending,
                    OrderDate = DateTime.Now,
                    OrderItems = [],
                };
                _context.Orders.Add(order);

                await _context.SaveChangesAsync();
            }
            return order;
        }

        public async Task AddOrderItem(string userName, Recipe recipe, int quantity = 1)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var order = await CurrentOrder(userName);
                order.OrderItems.Add(new OrderItem
                {
                    Recipe = recipe,
                    Quantity = quantity
                });

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

        public async Task<bool> ConfirmOrder(string userName)
        {
            return await UpdateStatus(userName, OrderStatus.Confirmed);
        }

        public async Task<bool> FinishOrder(string userName, float rating)
        {
            return await UpdateStatus(userName, OrderStatus.Done, rating);
        }

        private async Task<bool> UpdateStatus(string userName, OrderStatus status, float rating = 0)
        {
            var order = await GetPendingOrderByUserId(userName);
            if (order != null)
            {
                order.Rating = rating;
                order.Status = status;
                _context.Update(order);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
