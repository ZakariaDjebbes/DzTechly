using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Products;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using Infrastructure.Exceptions;

namespace Infrastructure.Services
{
    public class ConfigureService : IConfigureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfigureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Product>> GetProductsForTypeAsync(int typeId)
        {
            var spec = new ProductWithInformationsByTypeSpecification(typeId);
            var data = await _unitOfWork.Repository<Product>().GetListAllWithSpecAsync(spec);

            return data;
        }

        public async Task<IReadOnlyList<Product>> GetCPUWithCompatibilityForMotherboardAsync(int motherboardId)
        {
            List<Product> res = new List<Product>();
            var mspec = new ProductWithInformationsAllSpecification(motherboardId);
            var motherboard = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(mspec);

            if (motherboard == null)
            {
                throw new MissingProductException("Could not find a product with this id", motherboardId);
            }

            var types = await _unitOfWork.Repository<ProductType>().GetListAllAsync();
            var spec = new ProductWithInformationsByTypeSpecification(types.Where(x => x.Name == "CPU").First().Id);
            var cpus = await _unitOfWork.Repository<Product>().GetListAllWithSpecAsync(spec);

            var motherboardModel = motherboard
            .TechnicalSheet
            .ProductAddtionalInfos
            .Where(x => x.AdditionalInfoName.AdditionalInfoCategory.Name == "CPU Compatibility" && x.AdditionalInfoName.Name == "CPU Socket")
            .First().AdditionalInfoValue;

            foreach (var cpu in cpus)
            {
                var cpuModel = cpu.TechnicalSheet
                .ProductAddtionalInfos
                .Where(x => x.AdditionalInfoName.AdditionalInfoCategory.Name == "Main Caracteristics" && x.AdditionalInfoName.Name == "Processor Support")
                .First().AdditionalInfoValue;

                if (cpuModel == motherboardModel)
                    res.Add(cpu);
            }

            return res;
        }

        public async Task<IReadOnlyList<Product>> GetRAMWithCompatibilityForMotherboardAsync(int motherboardId)
        {
            List<Product> res = new List<Product>();
            var mspec = new ProductWithInformationsAllSpecification(motherboardId);
            var motherboard = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(mspec);

            if (motherboard == null)
            {
                throw new MissingProductException("Could not find a product with this id", motherboardId);
            }

            var types = await _unitOfWork.Repository<ProductType>().GetListAllAsync();
            var spec = new ProductWithInformationsByTypeSpecification(types.Where(x => x.Name == "RAM").First().Id);
            var rams = await _unitOfWork.Repository<Product>().GetListAllWithSpecAsync(spec);

            var motherboardFrequency = motherboard
            .TechnicalSheet
            .ProductAddtionalInfos
            .Where(x => x.AdditionalInfoName.AdditionalInfoCategory.Name == "Memory" && x.AdditionalInfoName.Name == "Memory Frequency")
            .First().AdditionalInfoValue;

            var motherboardSize = motherboard
            .TechnicalSheet
            .ProductAddtionalInfos
            .Where(x => x.AdditionalInfoName.AdditionalInfoCategory.Name == "Memory" && x.AdditionalInfoName.Name == "Maximum Memory Size")
            .First().AdditionalInfoValue;

            foreach (var ram in rams)
            {
                var ramFrequency = ram.TechnicalSheet
                .ProductAddtionalInfos
                .Where(x => x.AdditionalInfoName.AdditionalInfoCategory.Name == "Memory" && x.AdditionalInfoName.Name == "Frequency")
                .First().AdditionalInfoValue;

                var ramSize = ram.TechnicalSheet
                .ProductAddtionalInfos
                .Where(x => x.AdditionalInfoName.AdditionalInfoCategory.Name == "Memory" && x.AdditionalInfoName.Name == "Memory Size")
                .First().AdditionalInfoValue;

                if (int.Parse(ramFrequency) <= int.Parse(motherboardFrequency) && int.Parse(ramSize) <= int.Parse(motherboardSize))
                    res.Add(ram);
            }

            return res;
        }

        public async Task<IReadOnlyList<Product>> GetGPUWithCompatibilityForMotherboardAsync(int motherboardId)
        {
            var mspec = new ProductWithInformationsAllSpecification(motherboardId);
            var motherboard = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(mspec);

            if (motherboard == null)
            {
                throw new MissingProductException("Could not find a product with this id", motherboardId);
            }



            var motherboardAcceptsGPU = motherboard
            .TechnicalSheet
            .ProductAddtionalInfos
            .Where(x => x.AdditionalInfoName.AdditionalInfoCategory.Name == "Graphics" && x.AdditionalInfoName.Name == "GPU Socket")
            .First().AdditionalInfoValue.Equals("Yes") ? true : false;


            var types = await _unitOfWork.Repository<ProductType>().GetListAllAsync();
            var spec = new ProductWithInformationsByTypeSpecification(types.Where(x => x.Name == "GPU").First().Id);
            var gpus = await _unitOfWork.Repository<Product>().GetListAllWithSpecAsync(spec);

            if(motherboardAcceptsGPU)
                return gpus;
            else
                return null;
        }
    }
}