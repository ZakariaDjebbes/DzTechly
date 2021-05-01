﻿using System.Collections.Generic;

namespace API.Dtos
{
	public class CustomerCartDto
	{
		public string Id { get; set; }
		public List<CartItemDto> Items { get; set; }
		public int? DeliveryMethodId { get; set; }
	}
}