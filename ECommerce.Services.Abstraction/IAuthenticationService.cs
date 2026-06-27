using ECommerce.Shared.CommonResponses;
using ECommerce.Shared.DTOs.IdentityDTOs;
using ECommerce.Shared.DTOs.OrderDTOs;
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
        Task<Result<AddressDTO>> GetAddressAsync(string email);
        Task<Result<AddressDTO>> UpdateUserAddressAsync(string email,AddressDTO addressDTO);


    }
}
