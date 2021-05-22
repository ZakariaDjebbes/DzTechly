using System;

namespace API.Dtos.Product
{
    public class ReviewToReturnDto
    {
        public string Comment { get; set; }
        public int Stars { get; set; }
        public DateTimeOffset ReviewDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}