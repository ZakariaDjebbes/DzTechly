using Core.Entities.Order;

namespace Core.Specifications
{
    public class OrderWithItemsAndMethodSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndMethodSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            SetOrderByDescending(o => o.OrderDate);
        }

        public OrderWithItemsAndMethodSpecification(int id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        public OrderWithItemsAndMethodSpecification(int id, string email) 
        : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}