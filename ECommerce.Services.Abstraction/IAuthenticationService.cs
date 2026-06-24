using ECommerce.Shared.CommonResponses;
using ECommerce.Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Abstraction
{
    public interface IAuthenticationService
    {
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO );
        Task<bool> CheckEmailAsync( string email );
        Task<Result<UserDTO>> GetUserByEmailAsync(string email);


    }
}
