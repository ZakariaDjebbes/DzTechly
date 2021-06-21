using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Order;
using Core.Entities.Products;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using Core.Specifications.SpecificationParams;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(ICartRepository basketRepository, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _cartRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress, PersonalInformation info)
        {
            //get the basket
            var cart = await _cartRepository.GetCartAsync(basketId);

            //get the items
            List<OrderItem> items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, product.Price, item.Quantity);
                items.Add(orderItem);
            }

            //get the delivery method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //subtotal
            var subtotal = items.Sum(i => i.Price * i.Quantity);

            //order with payment intent id exists?
            var spec = new OrderByIntentIdSpecification(cart.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntentAsync(cart.PaymentIntentId);
            }

            //create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, cart.PaymentIntentId, info);
            _unitOfWork.Repository<Order>().Add(order);

            //save order to db
            var results = await _unitOfWork.Complete();
            if (results <= 0)
            {
                return null;
            }

            //return order
            return order;
        }

        public async Task<Pagination<Order>> GetAllOrders(OrderSpecificationParams specParams)
        {
            var spec = new OrderWithFiltersAndPanging(specParams);

            var data = await _unitOfWork.Repository<Order>().GetListAllWithSpecAsync(spec);
            var totalCount = data.Count;

            return new Pagination<Order>(specParams.PageIndex, specParams.PageSize, totalCount, data);
        }

        public async Task<DeliveryMethod> GetDeliveryMethodAsync(int id)
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(id);
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return _unitOfWork.Repository<DeliveryMethod>().GetListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndMethodSpecification(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var spec = new OrderWithItemsAndMethodSpecification(id);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
        }

        public async Task<Pagination<Order>> GetOrdersOfUserAsync(string buyerEmail, OrderSpecificationParams specParams)
        {
            var spec = new OrderWithFiltersAndPanging(buyerEmail, specParams);
            var countSpec = new OrderByEmailAndIdForCountSpecification(buyerEmail);

            var data = await _unitOfWork.Repository<Order>().GetListAllWithSpecAsync(spec);
            var totalCount = await _unitOfWork.Repository<Order>().CountAsync(countSpec);

            return new Pagination<Order>(specParams.PageIndex, specParams.PageSize, totalCount, data);
        }
    }
}