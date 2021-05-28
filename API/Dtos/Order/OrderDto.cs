using System.ComponentModel.DataAnnotations;
using API.Dtos.Identity;

namespace API.Dtos.Order
{
    public class OrderDto
    {
        [Required]
        public string cartId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }
        [Required]
        public PersonalInformationDto PersonalInformation { get; set; }      
    }
}