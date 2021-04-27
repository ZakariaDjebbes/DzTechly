using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using API.Controllers;
using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.Product;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using Core.Specifications.SpecificationParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReviewService _reviewService;

        public ProductsController(IUnitOfWork unitOfWork,
            IMapper mapper, UserManager<AppUser> userManager, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
        [FromQuery] ProductSpecificationParams specParams)
        {
            var specification = new ProductWithBrandAndTypeSpecification(specParams);
            var countSpec = new ProductWithFiltersForCountSpecification(specParams);

            var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
            var products = await _unitOfWork.Repository<Product>().GetListAllWithSpecAsync(specification);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct([Required] int id)
        {
            var specification = new ProductWithBrandAndTypeSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(specification);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeDto>>> GetProductTypes()
        {
            var specification = new ProductTypeWithCategorySpecification();
            var list = await _unitOfWork.Repository<ProductType>().GetListAllWithSpecAsync(specification);
            return Ok(_mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<ProductTypeDto>>(list));
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
        {
            return Ok(await _unitOfWork.Repository<ProductCategory>().GetListAllAsync());
        }

        [HttpPost("review")]
        [Authorize]
        public async Task<ActionResult<ReviewToReturnDto>> CreateReview(ReviewDto reviewDto)
        {
            var user = await _userManager.FindByClaimsWithAddressAsync(HttpContext.User);
            var review = await _reviewService.CreateReviewAsync(user.Id, reviewDto.ProductId.Value, reviewDto.Comment, reviewDto.Stars.Value);

            if (review == null)
            {
                return BadRequest(new ApiResponse(400, "Error reviewing this product"));
            }

            return Ok(_mapper.Map<Review, ReviewToReturnDto>(review));
        }

        [HttpGet("reviews/{id}")]
        public async Task<ActionResult<Pagination<ReviewToReturnDto>>> GetReviewsOfProduct([Required] int id,
            [FromQuery] ReviewSpecificationParams specParams)
        {
            ReviewOfProductWithPagingSpecification spec =
            new ReviewOfProductWithPagingSpecification(id, specParams);

            ReviewOfProductWithPagingSpecification countSpec = new ReviewOfProductWithPagingSpecification(id);

            var totalItems = await _unitOfWork.Repository<Review>().CountAsync(countSpec);
            var reviews = await _unitOfWork.Repository<Review>().GetListAllWithSpecAsync(spec);

            if (reviews == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            var data = _mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewToReturnDto>>(reviews);

            return
            Ok(new Pagination<ReviewToReturnDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }
    }
}