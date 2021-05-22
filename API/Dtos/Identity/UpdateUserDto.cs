using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Identity
{
    public class UpdateUserDto
    {
        [Required]
        public string[] Roles { get; set; }
        public string NewUserName { get; set; }
        [Required]
        public string Id { get; set; }
    }
}