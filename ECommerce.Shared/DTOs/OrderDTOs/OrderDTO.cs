using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.DTOs.OrderDTOs
{
    public record OrderDTO
    {
        public string BasketId { get; init; }
        public int DeliveryMethodId { get; init; }
        public AddressDTO Address { get; init; }
    }
}
