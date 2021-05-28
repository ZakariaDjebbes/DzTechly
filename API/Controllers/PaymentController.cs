using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities.Cart;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<IPaymentService> _logger;
        private readonly IConfiguration _config;

        private string whSecret = string.Empty;

        public PaymentController(IPaymentService paymentService, ILogger<IPaymentService> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _paymentService = paymentService;

            whSecret = _config["StripeSettings:LocalWHKey"];
        }

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<CustomerCart>> CreateOrUpdatePaymentIntent([Required] string cartId)
        {
            var cart = await _paymentService.CreateOrUpdatePaymentIntentAsync(cartId);
            if (cart == null)
            {
                return BadRequest(new ApiResponse(400, "Your cart has a problem"));
            }
            return cart;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent =
            EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], whSecret);

            PaymentIntent paymentIntent;
            Core.Entities.Order.Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment succeeded: ", paymentIntent.Id);
                    order = await _paymentService.UpdateOrderPaymentSuccessAsync(paymentIntent.Id);
                    _logger.LogInformation("Order updated to succeeded: ", order.Id);
                    break;

                case "payment_intent.payment_failed":
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed: ", paymentIntent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailedAsync(paymentIntent.Id);
                    _logger.LogInformation("Order updated to failed: ", order.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}