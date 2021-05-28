namespace Core.Entities.Order
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string city, string street, string zipCode, string wilaya)
        {
            City = city;
            Street = street;
            ZipCode = zipCode;
            Wilaya = wilaya;
        }

		public string City { get; set; }
		public string Street { get; set; }
		public string ZipCode { get; set; }
		public string Wilaya { get; set; }
    }
}