using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using API.Dtos.Product;
using API.Errors;
using AutoMapper;
using Core.Entities.Products;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ConfigureController : BaseApiController
    {
        private readonly IConfigureService _configureService;
        private readonly IMapper _mapper;
        public ConfigureController(IConfigureService configureService, IMapper mapper)
        {
            _mapper = mapper;
            _configureService = configureService;
        }

        [HttpGet("{typeId}")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProductsOfType([Required] int typeId)
        {
            var data = await _configureService.GetProductsForTypeAsync(typeId);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(data));
        }

        [HttpGet("CPU/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetCPUWithCompatibility([Required] int id)
        {
            var data = await _configureService.GetCPUWithCompatibilityForMotherboardAsync(id);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(data));
        }

        [HttpGet("RAM/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetRAMWithCompatibility([Required] int id)
        {
            var data = await _configureService.GetRAMWithCompatibilityForMotherboardAsync(id);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(data));
        }

        [HttpGet("GPU/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetGPUWithCompatibility([Required] int id)
        {
            var data = await _configureService.GetGPUWithCompatibilityForMotherboardAsync(id);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(data));
        }
    }
}