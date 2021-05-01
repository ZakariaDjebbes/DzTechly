using System.Threading.Tasks;
using Core.Entities.Cart;

namespace Core.Interfaces.Repositories
{
	public interface ICartRepository
	{
		Task<CustomerCart> GetCartAsync(string basketId);
		Task<CustomerCart> CreateOrUpdateCartAsync(CustomerCart customerBasket);
		Task<bool> DeleteCartAsync(string basketId);
	}
}
