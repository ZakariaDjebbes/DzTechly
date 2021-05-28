using System.Threading.Tasks;
using Core.Entities.Product;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System.Linq;

namespace Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> CreateOrUpdateReviewAsync(string userId, Product product, string comment, int stars)
        {
            Review review;
            
            if (product.Reviews.Where(x => x.AppUserId == userId).Any())
            {
                review = product.Reviews.Where(x => x.AppUserId == userId).First();
                review.Comment = comment;
                review.Stars = stars;
                _unitOfWork.Repository<Review>().Update(review);
            }
            else
            {
                review = new Review(userId, product.Id, comment, stars);
                _unitOfWork.Repository<Review>().Add(review);
            }


            var results = await _unitOfWork.Complete();

            if (results <= 0)
            {
                return null;
            }

            return review;
        }
    }
}