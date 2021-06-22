using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Products;

namespace Core.Interfaces.Services
{
    public interface IConfigureService
    {
        Task<IReadOnlyList<Product>> GetProductsForTypeAsync(int typeId);
        Task<IReadOnlyList<Product>> GetCPUWithCompatibilityForMotherboardAsync(int motherboardId);
        Task<IReadOnlyList<Product>> GetRAMWithCompatibilityForMotherboardAsync(int motherboardId);
        Task<IReadOnlyList<Product>> GetGPUWithCompatibilityForMotherboardAsync(int motherboardId);
    }
}