using System.Threading.Tasks;
using Core.Entities.Product;

namespace Core.Interfaces.Services
{
    public interface IReviewService
    {
        Task<Review> CreateOrUpdateReviewAsync(string userId, Product product, string comment, int stars);
    }
}