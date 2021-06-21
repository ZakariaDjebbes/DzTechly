using System.Collections.Generic;

namespace API.Dtos.Identity
{
    public class UserForAdministrationDto
    {
		public string Id { get; set; }
        public string UserName { get; set; }
        public ICollection<string> UserRoles { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}