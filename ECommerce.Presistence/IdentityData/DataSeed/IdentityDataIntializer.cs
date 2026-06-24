using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistence.IdentityData.DataSeed
{
    public class IdentityDataIntializer : IDataIntializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataIntializer> _logger;

        public IdentityDataIntializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<IdentityDataIntializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task IntializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                     await _roleManager.CreateAsync(new IdentityRole("Admin"));
                     await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        DisplayName = "Ahmed Asem",
                        UserName = "AhmedAsem",
                        Email = "asem4303@gmail.com",
                        PhoneNumber = "01012905054"
                    };

                    var User02 = new ApplicationUser
                    {
                        DisplayName = "Mohamed Asem",
                        UserName = "MohamedAsem",
                        Email = "MohamedAsem@gmail.com",
                        PhoneNumber = "01012905000"
                    };

                    await _userManager.CreateAsync(User01,"P@ssw0rd");
                    await _userManager.CreateAsync(User02,"P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(User02, "Admin");
                
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error while seeding Database , {ex.Message} happend");
            }
        }
    }
}
