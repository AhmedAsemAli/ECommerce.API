using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.DTOs.IdentityDTOs
{
    public record UserDTO(string Email, string DisplayName, string Token);
    
}
