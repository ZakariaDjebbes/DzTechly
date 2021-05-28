using System.Threading.Tasks;
using Core.Entities.Cart;
using Core.Entities.Order;

namespace Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<CustomerCart> CreateOrUpdatePaymentIntentAsync(string basketId);
        Task<Order> UpdateOrderPaymentSuccessAsync(string paymentIntentId);
        Task<Order> UpdateOrderPaymentFailedAsync(string paymentIntentId);
    }
}