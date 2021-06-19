using System.Collections.Generic;
using Core.Entities.Products;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public Address Address { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Product> WaitingProducts { get; set; }
    }
}
