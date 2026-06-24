using ECommerce.Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications.OrderSpecifications
{
    public class OrderSpecification:BaseSpecifications<Order ,Guid>
    {
        public OrderSpecification(string email):base(o=>o.UserEmail==email)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddOrderByDescending(x => x.OrderDate);
        }
        public OrderSpecification(Guid id, string email):base(o=>o.UserEmail==email&&o.Id==id)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
        }


    }
}
