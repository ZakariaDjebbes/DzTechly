using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Identity
{
    public class PersonalInformationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }
}