using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IOrderService
    {
        Task AddOrderItem(string userName, Recipe recipe, int quantity = 1);
        Task<bool> ConfirmOrder(string userName);
        Task<Order> CurrentOrder(string userName);
        Task<bool> FinishOrder(string userName, float rating);
        Task<Order?> GetOrderById(int id);
        Task<List<Order>> GetOrders(string userName);
        Task<Order?> GetPendingOrderByUserId(string userName);
    }
}