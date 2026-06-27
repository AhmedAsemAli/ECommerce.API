using ECommerce.Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications.OrderSpecifications
{
    internal class OrderWithPaymentIntentSpecification:BaseSpecifications<Order,Guid>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId):base(o=>o.PaymentIntentId==paymentIntentId)
        {
            
        }
    }
}
