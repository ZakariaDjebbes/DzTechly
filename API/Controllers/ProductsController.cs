using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Dtos.Product;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.Products;
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
            var specification = new ProductWithInformationsAllSpecification(specParams);
            var countSpec = new ProductWithFiltersForCountSpecification(specParams);

            var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
            var products = await _unitOfWork.Repository<Product>().GetListAllWithSpecAsync(specification);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct([Required] int id)
        {
            var specification = new ProductWithInformationsAllSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(specification);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpPost("waitingList")]
        [Authorize]
        public async Task<ActionResult<ProductToReturnDto>> AddToWaitingList([Required][FromQuery] int productId)
        {
            var user = await _userManager.FindByClaimsAsync(HttpContext.User);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var spec = new ProductWithInformationsAllSpecification(productId);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);

            if (product == null)
                return BadRequest(new ApiResponse(400, "Could not find this product"));

            if (product.WaitingList.Where(x => x.Id == user.Id).Any())
                return BadRequest(new ApiResponse(400, "You are already in the waiting list for this product"));

            product.WaitingList.Add(user);
            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeDto>>> GetProductTypes()
        {
            var specification = new ProductTypeWithCategorySpecification();
            var list = await _unitOfWork.Repository<ProductType>().GetListAllWithSpecAsync(specification);
            return Ok(_mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<ProductTypeDto>>(list));
        }

        [HttpGet("typesOfCategory")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeDto>>> GetProductTypesOfCategory([Required][FromQuery] int categoryId)
        {
            var specification = new ProductTypeOfCategorySpecification(categoryId);
            var list = await _unitOfWork.Repository<ProductType>().GetListAllWithSpecAsync(specification);
            return Ok(_mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<ProductTypeDto>>(list));
        }


        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
        {
            return Ok(await _unitOfWork.Repository<ProductCategory>().GetListAllAsync());
        }

        [HttpGet("additionalInfoNames")]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult<IReadOnlyList<AdditionalInfoNameDto>>> GetAdditionalInfoNamesOfType([Required][FromQuery] int typeId)
        {
            var spec = new AdditionalInfoNamesOfProductTypeSpecification(typeId);
            var res = await _unitOfWork.Repository<AdditionalInfoName>().GetListAllWithSpecAsync(spec);
            var list = _mapper.Map<IReadOnlyList<AdditionalInfoName>, IReadOnlyList<AdditionalInfoNameDto>>(res);

            return Ok(list);
        }

        [HttpPut]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(productToUpdateDto productDto)
        {
            //NO TIME TO USE AUTO MAPPER F DEADLINE SORRY
            var productTypes = await _unitOfWork.Repository<ProductType>().GetListAllAsync();
            var productTypeId = productTypes.Where(x => x.Name == productDto.ProductType).Select(x => x.Id).FirstOrDefault();

            var productCategories = await _unitOfWork.Repository<ProductCategory>().GetListAllAsync();
            var ProductCategoryId = productCategories.Where(x => x.Name == productDto.ProductCategory).Select(x => x.Id).FirstOrDefault();

            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                ProductCategoryId = ProductCategoryId,
                ProductTypeId = productTypeId,
                PictureUrl = productDto.PictureUrl
            };

            var infos = new List<ProductAdditionalInfo>();

            foreach (var item in productDto.TechnicalSheet.ProductAddtionalInfos.Values)
            {
                foreach (var val in item)
                    infos.Add(new ProductAdditionalInfo
                    {
                        AdditionalInfoValue = val.AdditionalInfoValue,
                        AdditionalInfoNameId = val.AdditionalInfoNameId,
                        TechnicalSheetId = productDto.TechnicalSheet.Id,
                        Id = val.Id
                    });
            }

            product.TechnicalSheet = new TechnicalSheet
            {
                Id = productDto.TechnicalSheet.Id,
                ProductId = product.Id,
                ProductAddtionalInfos = infos,
                LastUpdateDate = DateTimeOffset.Now
            };

            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.Complete();

            var spec = new ProductWithInformationsAllSpecification(product.Id);
            var res = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);
            return _mapper.Map<Product, ProductToReturnDto>(res);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult<ProductToReturnDto>> AddProduct(ProductToCreate addProductDto)
        {
            //Major design flaw in the data entites, to fix this bug need to change the current data format...
            //AdditionalInfo => Infoname => InfoCategory => InfoNames...
            var product = _mapper.Map<ProductToCreate, Product>(addProductDto);
            var productAdditionalInfos = _mapper.Map<ICollection<AdditionalInfoValueDto>, ICollection<ProductAdditionalInfo>>(addProductDto.AdditionalInformations);

            var sheet = new TechnicalSheet
            {
                ProductAddtionalInfos = productAdditionalInfos.ToList(),
                ReferenceDate = DateTimeOffset.Now,
                LastUpdateDate = DateTimeOffset.Now,
                Product = product
            };

            foreach (var info in productAdditionalInfos)
            {
                info.TechnicalSheet = sheet;
                info.AdditionalInfoName.ProductTypeId = product.ProductTypeId;

                if (info.AdditionalInfoNameId != 0)
                {
                    info.AdditionalInfoName = null;
                }
            }

            product.TechnicalSheet = sheet;
            product.Reviews = new List<Review>();

            _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.Complete();

            var res = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            return _mapper.Map<Product, ProductToReturnDto>(res);
        }

        [HttpDelete]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult> DeleteProduct([Required][FromQuery] int productId)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);

            if (product == null)
                return BadRequest(new ApiResponse(400, "This product does not exist"));

            _unitOfWork.Repository<Product>().Delete(product);
            await _unitOfWork.Complete();
            product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);

            if (product != null)
                return BadRequest(new ApiResponse(400, "Error deleting this product"));

            return Ok();
        }

        [HttpPost("review")]
        [Authorize]
        public async Task<ActionResult<ReviewToReturnDto>> CreateReview(ReviewDto reviewDto)
        {
            var user = await _userManager.FindByClaimsWithAddressAndInfoAsync(HttpContext.User);
            var spec = new ProductWithInformationsAllSpecification(reviewDto.ProductId.Value);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);

            if (product == null)
            {
                return BadRequest(new ApiResponse(400, "No such product exists"));
            }

            var review = await _reviewService.CreateOrUpdateReviewAsync(user.Id, product, reviewDto.Comment, reviewDto.Stars.Value);

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