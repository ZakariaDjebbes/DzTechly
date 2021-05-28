using System;
namespace Core.Entities.Order
{
    public class PersonalInformation
    {
        public PersonalInformation()
        {

        }

        public PersonalInformation(string firstName, string lastName, string birthDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
    }
}