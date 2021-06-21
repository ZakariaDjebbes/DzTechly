using System;

namespace API.Dtos.Product
{
    public class ReviewToReturnDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Stars { get; set; }
        public DateTimeOffset ReviewDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}