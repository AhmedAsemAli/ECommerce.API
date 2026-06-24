using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Shared.DTOs.IdentityDTOs
{
    public record RegisterDTO([EmailAddress] string Email,string DisplayName,string UserName,string Password,[Phone]string PhoneNumber);
    
}
