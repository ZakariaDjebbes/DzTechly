using System;

namespace Core.Entities.Identity
{
    public class PersonalInformation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
        public string NationalCardNumber { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
    }
}