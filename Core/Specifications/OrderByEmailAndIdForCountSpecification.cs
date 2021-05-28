using Core.Entities.Order;

namespace Core.Specifications
{
    public class OrderByEmailAndIdForCountSpecification : BaseSpecification<Order>
    {
        public OrderByEmailAndIdForCountSpecification(string email) : base(o => o.BuyerEmail == email)
        {
        }

        public OrderByEmailAndIdForCountSpecification(int id, string email)
        : base(o => o.Id == id && o.BuyerEmail == email)
        {
        }
    }
}