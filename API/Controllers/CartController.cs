using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Cart;
using Core.Entities.Product;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById([Required] string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);

            return Ok(cart ?? new CustomerCart(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCartDto customerCart)
        {
            var mappedCart = _mapper.Map<CustomerCartDto, CustomerCart>(customerCart);
            var cart = await _cartRepository.CreateOrUpdateCartAsync(mappedCart);

            return Ok(cart);
        }

        [HttpDelete]
        public async Task DeleteCart([Required] string id)
        {
            await _cartRepository.DeleteCartAsync(id);
        }
    }
}
