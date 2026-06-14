using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.DTOs.BasketDTOs
{
    public record BasketDTO(string Id, ICollection<BasketItemDTO> items);
   
}
