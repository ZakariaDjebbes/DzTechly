using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities.Cart;
using Core.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Repositories
{
	public class CartRepository : ICartRepository
	{
		private readonly IDatabase _database;
		private readonly IConfiguration _config;

		public CartRepository(IConnectionMultiplexer redis, IConfiguration config)
		{
			_database = redis.GetDatabase();
			_config = config;
		}

		public async Task<bool> DeleteCartAsync(string cartId)
		{
			return await _database.KeyDeleteAsync(cartId);
		}

		public async Task<CustomerCart> GetCartAsync(string cartId)
		{
			var data = await _database.StringGetAsync(cartId);

			return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(data.ToString());
		}

		public async Task<CustomerCart> CreateOrUpdateCartAsync(CustomerCart cart)
		{
			double daysToLive = Convert.ToDouble(_config.GetValue<int>("CartLifeSpanInMinutes"));

			var created = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart),
				TimeSpan.FromMinutes(daysToLive));

			return created ? await GetCartAsync(cart.Id) : null;
		}
	}
}
