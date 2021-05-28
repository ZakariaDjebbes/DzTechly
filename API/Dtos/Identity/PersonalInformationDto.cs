using System;

namespace API.Dtos.Identity
{
    public class PersonalInformationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }
}