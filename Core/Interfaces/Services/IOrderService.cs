using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Order;
using Core.Specifications;
using Core.Specifications.SpecificationParams;

namespace Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress, PersonalInformation info);
        Task<Pagination<Order>> GetOrdersOfUserAsync(string buyerEmail, OrderSpecificationParams specParams);
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<DeliveryMethod> GetDeliveryMethodAsync(int id);
        Task<Pagination<Order>> GetAllOrders(OrderSpecificationParams specParams);
        Task<Order> GetOrderByIdAsync(int id);
    }
}