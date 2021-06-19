using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Cart;
using Core.Entities.Order;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepository;

        public PaymentService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            this._cartRepository = cartRepository;
            this._unitOfWork = unitOfWork;
            this._config = config;
        }

        public async Task<CustomerCart> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var cart = await _cartRepository.GetCartAsync(basketId);

            if (cart == null)
            {
                return null;
            }

            var shippingPrice = 0m;

            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(cart.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in cart.Items)
            {
                var productItem = await _unitOfWork.Repository<Core.Entities.Products.Product>().GetByIdAsync(item.Id);

                if (productItem == null)
                    throw new MissingProductException($"The product {item.ProductName} is no longer available. The product is going to be removed from you cart.", item.Id);

                if (productItem.Quantity <= 0)
                    throw new ProductNoLongerInStockException($"The product {item.ProductName} is not in stock. The product is going to be removed from you cart.", item.Id);

                if (item.Price != productItem.Price)
                    item.Price = productItem.Price;
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var opts = new PaymentIntentCreateOptions
                {
                    Amount = (long)(shippingPrice * 100) + (long)(cart.Items.Sum(i => (i.Price * 100) * i.Quantity)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(opts);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var opts = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(shippingPrice * 100) + (long)(cart.Items.Sum(i => (i.Price * 100) * i.Quantity)),
                    Currency = "usd",
                };

                await service.UpdateAsync(cart.PaymentIntentId, opts);
            }

            await _cartRepository.CreateOrUpdateCartAsync(cart);

            return cart;
        }

        public async Task<Core.Entities.Order.Order> UpdateOrderPaymentSuccessAsync(string paymentIntentId)
        {
            var spec = new OrderByIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Core.Entities.Order.Order>().GetEntityWithSpecAsync(spec);

            if (order == null)
            {
                return null;
            }

            order.Status = OrderStatus.PayementReceived;
            _unitOfWork.Repository<Core.Entities.Order.Order>().Update(order);

            await _unitOfWork.Complete();
            return order;
        }

        public async Task<Core.Entities.Order.Order> UpdateOrderPaymentFailedAsync(string paymentIntentId)
        {
            var spec = new OrderByIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Core.Entities.Order.Order>().GetEntityWithSpecAsync(spec);

            if (order == null)
            {
                return null;
            }

            order.Status = OrderStatus.PayementFailed;

            await _unitOfWork.Complete();
            return order;
        }
    }
}