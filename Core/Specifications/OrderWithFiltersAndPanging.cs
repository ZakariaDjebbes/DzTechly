using System;
using Core.Entities.Order;
using Core.Specifications.SpecificationParams;

namespace Core.Specifications
{
    public class OrderWithFiltersAndPanging : BaseSpecification<Order>
    {
        public OrderWithFiltersAndPanging(string email, OrderSpecificationParams specParams)
            : base(x=> email.Equals(x.BuyerEmail))
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "dateAsc":
                    SetOrderBy(x => x.OrderDate);
                    break;
                case "idAsc":
                    SetOrderBy(x => x.Id);
                    break;
                case "idDesc":
                    SetOrderByDescending(x => x.Id);
                    break;
                default:
                    SetOrderByDescending(x => x.OrderDate);
                    break;
            }
        }

        public OrderWithFiltersAndPanging(OrderSpecificationParams specParams)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "dateAsc":
                    SetOrderBy(x => x.OrderDate);
                    break;
                case "idAsc":
                    SetOrderBy(x => x.Id);
                    break;
                case "idDesc":
                    SetOrderByDescending(x => x.Id);
                    break;
                default:
                    SetOrderByDescending(x => x.OrderDate);
                    break;
            }
        }
    }
}