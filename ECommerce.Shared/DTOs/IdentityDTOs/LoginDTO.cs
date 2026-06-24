using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Shared.DTOs.IdentityDTOs
{
    public record LoginDTO([EmailAddress] string Email, string Password);
   
}
