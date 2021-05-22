using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Identity
{
	public class AddressDto
	{
		[Required]
		public string City { get; set; }
		[Required]
		public string Street { get; set; }
		[Required]
		public string Zipcode { get; set; }
		[Required]
		public string Wilaya { get; set; }
	}
}
