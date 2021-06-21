using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Errors;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces.Services;
using API.Dtos.Order;
using API.Dtos.Identity;
using Core.Specifications.SpecificationParams;
using Core.Specifications;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.GetEmailFromClaims();
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var info = _mapper.Map<PersonalInformationDto, PersonalInformation>(orderDto.PersonalInformation);

            var order =
            await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.cartId, address, info);

            if (order == null)
                return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<OrderToReturnDto>>> GetOrdersOfUser([FromQuery] OrderSpecificationParams specParams)
        {
            var email = HttpContext.User?.GetEmailFromClaims();
            var orders = await _orderService.GetOrdersOfUserAsync(email, specParams);

            return Ok(_mapper.Map<Pagination<Order>, Pagination<OrderToReturnDto>>(orders));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById([Required] int id)
        {
            var email = HttpContext.User?.GetEmailFromClaims();
            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [Authorize]
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetAdministrationOrderById([Required] int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [Authorize(Policy = "RequireAdministration")]
        [HttpGet("all")]
        public async Task<ActionResult<OrderToReturnDto>> GetAllOrders([FromQuery] OrderSpecificationParams specParams)
        {
            var orders = await _orderService.GetAllOrders(specParams);

            return Ok(_mapper.Map<Pagination<Order>, Pagination<OrderToReturnDto>>(orders));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var methods = await _orderService.GetDeliveryMethodsAsync();

            return Ok(methods);
        }

        [HttpGet("deliveryMethod/{id}")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod([Required] int id)
        {
            var method = await _orderService.GetDeliveryMethodAsync(id);

            return Ok(method);
        }
    }
}